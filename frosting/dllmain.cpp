// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "shell32ex.h"
#include "detours.h"
#include "wil/registry.h"
#include "unknwn.h"
#include "ntdllex.h"
#include "preferencefx.h"
#include "memfind.h"
#include "constants.h"
#pragma comment(lib, "detours.lib")

HMODULE g_hModule = NULL, g_hTaskbarMod = NULL;
bool g_bLoadedInExplorer = false, g_bMultiMonEnabled = false, g_bShowLabels = false;
int g_dwGroupingMode = 0, g_dwMMGroupingMode = 0, g_dwMonitorCount = 0;
void** g_pPrimaryTaskbarSettings = nullptr;
ptrdiff_t g_nSettings5ToBaseClassDiff = 0;
HANDLE g_hWnfDisplayChangeSub = NULL;
wil::unique_registry_watcher g_cPrefRegWatcher = nullptr;

wchar_t* (__stdcall* RealGetOverlayIconDescription)(void** This) = NULL;
void(__stdcall* RealTaskbarSettingsInitialize)(void** This) = NULL;
HRESULT(__stdcall* Realget_GroupingMode)(void** This, int* groupingMode) = NULL;
HRESULT(__stdcall* Realget_ShowLabels)(void** This, bool* showLabels) = NULL;
NTSTATUS(NTAPI* RealRtlQueryFeatureConfiguration)(_In_ ULONG FeatureId,
    _In_ RTL_FEATURE_CONFIGURATION_TYPE FeatureType,
    _Inout_ PULONGLONG ChangeStamp,
    _In_ PRTL_FEATURE_CONFIGURATION FeatureConfiguration
    ) = NULL;
HRESULT(__stdcall* RealTaskGroupGetTitleText)(void* This, void* taskItem, wchar_t* buffer, int length) = NULL;
void(__stdcall* RealOnStartTaskbarApiSurface)(void* This) = NULL;
void(__stdcall* RealStartLabelAnimation)(void* This, void** pUIElement, float fZero, float fOne) = NULL;

wchar_t* __stdcall MineGetOverlayIconDescription(void** This)
{
    auto storedDescription = (wchar_t*)*(This + 21);
    if (!storedDescription)
        return (wchar_t*)DummyEmptyString;
    else
        return storedDescription;
}

HRESULT __stdcall Mineget_GroupingMode(void** This, int* pGroupingMode)
{
    auto baseClass = This + g_nSettings5ToBaseClassDiff;
    auto isThisPrimaryTaskbar = !g_bMultiMonEnabled || baseClass == g_pPrimaryTaskbarSettings;
    *pGroupingMode = isThisPrimaryTaskbar ? g_dwGroupingMode : g_dwMMGroupingMode;
    return S_OK;
}

HRESULT __stdcall Mineget_ShowLabels(void** This, bool* pShowLabels)
{
    *pShowLabels = g_bShowLabels;
    return S_OK;
}

void __stdcall MineTaskbarSettingsInitialize(void** This)
{
    RealTaskbarSettingsInitialize(This);

    if (g_pPrimaryTaskbarSettings)
        return;

    // Skipping over heap_implements vtable pointer and ref count property
    auto pTaskbarSettings = (IUnknown*)(This + 2);
    void** pTaskbarSettings5 = nullptr;
    auto res = pTaskbarSettings->QueryInterface(IID_ITaskbarSettings5, (void**)&pTaskbarSettings5);
    if (res != S_OK)
        return;

    g_pPrimaryTaskbarSettings = This;
    g_nSettings5ToBaseClassDiff = This - pTaskbarSettings5;

    pTaskbarSettings5 = (void**)*pTaskbarSettings5;
    Realget_GroupingMode = (HRESULT(__stdcall*)(void**, int*)) * (pTaskbarSettings5 + 6);
    Realget_ShowLabels = (HRESULT(__stdcall*)(void**, bool*)) * (pTaskbarSettings5 + 7);

    DetourTransactionBegin();
    DetourUpdateThread(GetCurrentThread());
    DetourAttach(&(PVOID&)Realget_GroupingMode, Mineget_GroupingMode);
    DetourAttach(&(PVOID&)Realget_ShowLabels, Mineget_ShowLabels);
    DetourTransactionCommit();

    pTaskbarSettings->Release();
}

NTSTATUS NTAPI MineRtlQueryFeatureConfiguration(
    _In_ ULONG FeatureId,
    _In_ RTL_FEATURE_CONFIGURATION_TYPE FeatureType,
    _Inout_ PULONGLONG ChangeStamp,
    _In_ PRTL_FEATURE_CONFIGURATION FeatureConfiguration
)
{
    if (FeatureId != 29785186)
        return RealRtlQueryFeatureConfiguration(FeatureId,
            FeatureType,
            ChangeStamp,
            FeatureConfiguration);

    ZeroMemory(FeatureConfiguration, sizeof(RTL_FEATURE_CONFIGURATION));
    FeatureConfiguration->FeatureId = FeatureId;
    FeatureConfiguration->Priority = 8;
    FeatureConfiguration->EnabledState = 2;
    *ChangeStamp = 1;
    return STATUS_SUCCESS;
}

HRESULT __stdcall MineTaskGroupGetTitleText(void* This, void* pTaskItem, wchar_t* pText, int dwSize)
{
    auto hr = RealTaskGroupGetTitleText(This, pTaskItem, pText, dwSize);
    if (hr == E_FAIL)
        return ERROR_SUCCESS;
    return hr;
}

void __stdcall MineStartLabelAnimation(void* This, void** pUIElement, float fZero, float fOne)
{
    if (*pUIElement != nullptr)
        RealStartLabelAnimation(This, pUIElement, fZero, fOne);
}

void __stdcall MineOnStartTaskbarApiSurface(void* This)
{
    RealOnStartTaskbarApiSurface(This);

    if (RealStartLabelAnimation)
        return;
    HMODULE tbViewMod;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT, L"Taskbar.View.dll", &tbViewMod))
        return;

    auto ntHeader = (PIMAGE_NT_HEADERS)((PBYTE)tbViewMod + ((PIMAGE_DOS_HEADER)tbViewMod)->e_lfanew);
    auto codeStart = (PBYTE)ntHeader + ntHeader->OptionalHeader.BaseOfCode;
    auto codeLength = ntHeader->OptionalHeader.SizeOfCode;
    auto locStartLabelAnimation = FindMasked128BInMemory(codeStart, codeLength, 0x8B48F2280FFB280F, 0x000000E8F98B48DA, -1, 0x000000FFFFFFFFFF, -0x27);
    if (locStartLabelAnimation)
    {
        RealStartLabelAnimation = (void(__stdcall*)(void*, void**, float, float))locStartLabelAnimation;

        DetourTransactionBegin();
        DetourUpdateThread(GetCurrentThread());
        DetourAttach(&(PVOID&)RealStartLabelAnimation, MineStartLabelAnimation);
        DetourTransactionCommit();
    }
}

bool PrefUpdateGroupingMode()
{
    return PrefUpdate(L"TaskbarGlomLevel", &g_dwGroupingMode, 0);
}

bool PrefUpdateMultiMonEnabled()
{
    return PrefUpdate(L"MMTaskbarEnabled", &g_bMultiMonEnabled, false);
}

bool PrefUpdateMMGroupingMode()
{
    return PrefUpdate(L"MMTaskbarGlomLevel", &g_dwMMGroupingMode, 0);
}

bool PrefUpdateShowLabels()
{
    return PrefUpdate(L"TaskbarShowLabels", &g_bShowLabels, false);
}

NTSTATUS NTAPI DxDisplayConfigChangeCallback(
    _In_ WNF_STATE_NAME StateName,
    _In_ DWORD ChangeStamp,
    _In_ PWNF_TYPE_ID TypeId,
    _In_opt_ PVOID CallbackContext,
    _In_ PVOID Buffer,
    _In_ ULONG BufferSize
)
{
    g_dwMonitorCount = GetSystemMetrics(SM_CMONITORS);
    return STATUS_SUCCESS;
}

bool IsRegsvr32()
{
    wchar_t exePath[MAX_PATH];
    DWORD dwLength = MAX_PATH;
    QueryFullProcessImageNameW(GetCurrentProcess(), 0, exePath, &dwLength);

    auto exeName = wcsrchr(exePath, '\\');
    if (!exeName)
        return false;

    if (!_wcsicmp(exeName + 1, L"regsvr32.exe"))
        return true;

    return false;
}

#pragma comment(linker, "/export:DllRegisterServer=_DllRegisterServer,PRIVATE")
STDAPI _DllRegisterServer()
{
    DWORD dwLastError = ERROR_SUCCESS;
    HKEY hKey = NULL;
    DWORD dwSize = 0;
    wchar_t wszFilename[MAX_PATH];

    if (!dwLastError)
    {
        if (!GetModuleFileNameW(g_hModule, wszFilename, MAX_PATH))
        {
            dwLastError = GetLastError();
        }
    }
    if (!dwLastError)
    {
        dwLastError = RegCreateKeyExW(
            HKEY_CURRENT_USER,
            L"SOFTWARE\\Classes\\CLSID\\" TEXT(UX_CLSID) L"\\InProcServer32",
            0,
            NULL,
            REG_OPTION_NON_VOLATILE,
            KEY_WRITE | KEY_WOW64_64KEY,
            NULL,
            &hKey,
            NULL
        );
        if (hKey == NULL || hKey == INVALID_HANDLE_VALUE)
        {
            hKey = NULL;
        }
        if (hKey)
        {
            dwLastError = RegSetValueExW(
                hKey,
                NULL,
                0,
                REG_SZ,
                (PBYTE)wszFilename,
                (DWORD)((wcslen(wszFilename) + 1) * sizeof(wchar_t))
            );
            dwLastError = RegSetValueExW(
                hKey,
                L"ThreadingModel",
                0,
                REG_SZ,
                (PBYTE)L"Apartment",
                10 * sizeof(wchar_t)
            );
            RegCloseKey(hKey);
        }
    }
    if (!dwLastError)
    {
        dwLastError = RegCreateKeyExW(
            HKEY_CURRENT_USER,
            L"SOFTWARE\\Classes\\Drive\\shellex\\FolderExtensions\\" TEXT(UX_CLSID),
            0,
            NULL,
            REG_OPTION_NON_VOLATILE,
            KEY_WRITE | KEY_WOW64_64KEY,
            NULL,
            &hKey,
            NULL
        );
        if (hKey == NULL || hKey == INVALID_HANDLE_VALUE)
        {
            hKey = NULL;
        }
        if (hKey)
        {
            DWORD dwDriveMask = 255;
            dwLastError = RegSetValueExW(
                hKey,
                L"DriveMask",
                0,
                REG_DWORD,
                (PBYTE)&dwDriveMask,
                sizeof(DWORD)
            );
            RegCloseKey(hKey);
        }
    }

    return dwLastError == 0 ? S_OK : HRESULT_FROM_WIN32(dwLastError);
}

#pragma comment(linker, "/export:DllUnregisterServer=_DllUnregisterServer,PRIVATE")
STDAPI _DllUnregisterServer()
{
    DWORD dwLastError = ERROR_SUCCESS;
    HKEY hKey = NULL;
    DWORD dwSize = 0;
    wchar_t wszFilename[MAX_PATH];

    if (!dwLastError)
    {
        if (!GetModuleFileNameW(g_hModule, wszFilename, MAX_PATH))
        {
            dwLastError = GetLastError();
        }
    }
    if (!dwLastError)
    {
        dwLastError = RegOpenKeyW(
            HKEY_CURRENT_USER,
            L"SOFTWARE\\Classes\\CLSID\\" TEXT(UX_CLSID),
            &hKey
        );
        if (hKey == NULL || hKey == INVALID_HANDLE_VALUE)
        {
            hKey = NULL;
        }
        if (hKey)
        {
            dwLastError = RegDeleteTreeW(
                hKey,
                0
            );
            RegCloseKey(hKey);
            if (!dwLastError)
            {
                RegDeleteTreeW(
                    HKEY_CURRENT_USER,
                    L"SOFTWARE\\Classes\\CLSID\\" TEXT(UX_CLSID)
                );
            }
        }
    }
    if (!dwLastError)
    {
        dwLastError = RegOpenKeyW(
            HKEY_CURRENT_USER,
            L"SOFTWARE\\Classes\\Drive\\shellex\\FolderExtensions\\" TEXT(UX_CLSID),
            &hKey
        );
        if (hKey == NULL || hKey == INVALID_HANDLE_VALUE)
        {
            hKey = NULL;
        }
        if (hKey)
        {
            dwLastError = RegDeleteTreeW(
                hKey,
                0
            );
            RegCloseKey(hKey);
            if (!dwLastError)
            {
                RegDeleteTreeW(
                    HKEY_CURRENT_USER,
                    L"SOFTWARE\\Classes\\Drive\\shellex\\FolderExtensions\\" TEXT(UX_CLSID)
                );
            }
        }
    }

    return dwLastError == 0 ? S_OK : HRESULT_FROM_WIN32(dwLastError);
}

#pragma comment(linker, "/export:DllCanUnloadNow=_DllCanUnloadNow,PRIVATE")
__control_entrypoint(DllExport)
STDAPI _DllCanUnloadNow(void)
{
    return g_bLoadedInExplorer ? S_FALSE : S_OK;
}

#pragma comment(linker, "/export:DllGetClassObject=_DllGetClassObject,PRIVATE")
_Check_return_
STDAPI _DllGetClassObject(_In_ REFCLSID rclsid, _In_ REFIID riid, _Outptr_ LPVOID FAR* ppv)
{
    // Don't attempt to patch if Taskbar already loaded
    HMODULE taskbarMod;
    if (GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT, L"Taskbar.dll", &taskbarMod))
        return E_NOINTERFACE;
    taskbarMod = LoadLibraryW(L"Taskbar.dll");
    if (!taskbarMod)
        return E_NOINTERFACE;
    g_hTaskbarMod = taskbarMod;

    auto ntHeader = (PIMAGE_NT_HEADERS)((PBYTE)taskbarMod + ((PIMAGE_DOS_HEADER)taskbarMod)->e_lfanew);
    auto codeStart = (PBYTE)ntHeader + ntHeader->OptionalHeader.BaseOfCode;
    auto codeLength = ntHeader->OptionalHeader.SizeOfCode;

    auto locGetOverlayIconDescription = Find64BInMemory(codeStart, codeLength, 0xC3000000A8818B48, 0);
    if (!locGetOverlayIconDescription)
        return E_NOINTERFACE;
    RealGetOverlayIconDescription = (wchar_t* (__stdcall*)(void**))locGetOverlayIconDescription;

    auto locTaskbarSettingsInitialize = Find64BInMemory(codeStart, codeLength, 0x8B48FF3345F98B48, -0x37);
    if (locTaskbarSettingsInitialize)
        RealTaskbarSettingsInitialize = (void(__stdcall*)(void**))locTaskbarSettingsInitialize;

    auto locTaskGroupGetTitleText = Find64BInMemory(codeStart, codeLength, 0xE96349FF334530EC, -0x16);
    if (locTaskGroupGetTitleText)
        RealTaskGroupGetTitleText = (HRESULT(__stdcall*)(void*, void*, wchar_t*, int))locTaskGroupGetTitleText;

    // After this function runs the correct undocked Taskbar.View will be loaded in memory for us
    auto locOnStartTaskbarApiSurface = Find64BInMemory(codeStart, codeLength, 0x00000478BAC8B60F, -0x1D);
    if (locOnStartTaskbarApiSurface)
    {
        if (*(uint64_t*)(locOnStartTaskbarApiSurface + 1) == 0xD98B4820EC834857) // RS prologue
            locOnStartTaskbarApiSurface -= 4;
        else if (*(uint64_t*)locOnStartTaskbarApiSurface != 0x8B4820EC83485340) // NI prologue
            locOnStartTaskbarApiSurface = nullptr;
        if (locOnStartTaskbarApiSurface)
            RealOnStartTaskbarApiSurface = (void(__stdcall*)(void*))locOnStartTaskbarApiSurface;
    }

    RealRtlQueryFeatureConfiguration = RtlQueryFeatureConfiguration;

    DetourTransactionBegin();
    DetourUpdateThread(GetCurrentThread());
    DetourAttach(&(PVOID&)RealGetOverlayIconDescription, MineGetOverlayIconDescription);
    if (RealTaskbarSettingsInitialize)
        DetourAttach(&(PVOID&)RealTaskbarSettingsInitialize, MineTaskbarSettingsInitialize);
    if (RealTaskGroupGetTitleText)
        DetourAttach(&(PVOID&)RealTaskGroupGetTitleText, MineTaskGroupGetTitleText);
    if (RealOnStartTaskbarApiSurface)
        DetourAttach(&(PVOID&)RealOnStartTaskbarApiSurface, MineOnStartTaskbarApiSurface);
    DetourAttach(&(PVOID&)RealRtlQueryFeatureConfiguration, MineRtlQueryFeatureConfiguration);
    DetourTransactionCommit();

    PrefUpdateGroupingMode();
    PrefUpdateMultiMonEnabled();
    PrefUpdateMMGroupingMode();
    PrefUpdateShowLabels();

    g_cPrefRegWatcher = wil::make_registry_watcher(
        HKEY_CURRENT_USER,
        L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
        false,
        [](wil::RegistryChangeKind changeType)
        {
            if (PrefUpdateShowLabels())
                goto SendTrayMessage;

            if (PrefUpdateGroupingMode())
                goto SendTrayMessage;

            if (PrefUpdateMultiMonEnabled())
                return;

            if (PrefUpdateMMGroupingMode())
            {
                if (g_bMultiMonEnabled && g_dwMonitorCount > 1)
                    goto SendTrayMessage;
                return;
            }

            return;

        SendTrayMessage:
            SendNotifyMessageW(HWND_BROADCAST, 0x1Au, NULL, (LPARAM)L"TraySettings");
        });

    RtlSubscribeWnfStateChangeNotification(&g_hWnfDisplayChangeSub, WNF_DX_DISPLAY_CONFIG_CHANGE_NOTIFICATION, 0,
        DxDisplayConfigChangeCallback, NULL, NULL, NULL, 0);

    g_bLoadedInExplorer = true;

    return E_NOINTERFACE;
}

BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    PVOID lpReserved
)
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        if (!IsDesktopExplorerProcess() && !IsRegsvr32())
            return FALSE;
        DisableThreadLibraryCalls(hModule);
        g_hModule = hModule;
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
        break;
    case DLL_PROCESS_DETACH:
        if (g_cPrefRegWatcher)
            g_cPrefRegWatcher.reset();
        if (g_hWnfDisplayChangeSub)
            RtlUnsubscribeWnfStateChangeNotification(g_hWnfDisplayChangeSub);
        DetourTransactionBegin();
        DetourUpdateThread(GetCurrentThread());
        if (RealRtlQueryFeatureConfiguration)
            DetourDetach(&(PVOID&)RealRtlQueryFeatureConfiguration, MineRtlQueryFeatureConfiguration);
        if (RealGetOverlayIconDescription)
            DetourDetach(&(PVOID&)RealGetOverlayIconDescription, MineGetOverlayIconDescription);
        if (RealTaskbarSettingsInitialize)
            DetourDetach(&(PVOID&)RealTaskbarSettingsInitialize, MineTaskbarSettingsInitialize);
        if (RealTaskGroupGetTitleText)
            DetourDetach(&(PVOID&)RealTaskGroupGetTitleText, MineTaskGroupGetTitleText);
        if (Realget_GroupingMode)
            DetourDetach(&(PVOID&)Realget_GroupingMode, Mineget_GroupingMode);
        if (Realget_ShowLabels)
            DetourDetach(&(PVOID&)Realget_ShowLabels, Mineget_ShowLabels);
        if (RealOnStartTaskbarApiSurface)
            DetourDetach(&(PVOID&)RealOnStartTaskbarApiSurface, MineOnStartTaskbarApiSurface);
        if (RealStartLabelAnimation)
            DetourDetach(&(PVOID&)RealStartLabelAnimation, MineStartLabelAnimation);
        DetourTransactionCommit();
        if (g_hTaskbarMod)
            FreeLibrary(g_hTaskbarMod);
        break;
    }
    return TRUE;
}


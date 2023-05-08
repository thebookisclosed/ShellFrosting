#pragma once
#pragma comment(lib, "ntdllex.lib")
typedef enum _RTL_FEATURE_CONFIGURATION_TYPE
{
    RtlFeatureConfigurationBoot = 0,
    RtlFeatureConfigurationRuntime = 1,
    RtlFeatureConfigurationCount = 2
} RTL_FEATURE_CONFIGURATION_TYPE, * PRTL_FEATURE_CONFIGURATION_TYPE;

typedef struct _RTL_FEATURE_CONFIGURATION
{
    unsigned int FeatureId;
    struct
    {
        unsigned int Priority : 4;
        unsigned int EnabledState : 2;
        unsigned int IsWexpConfiguration : 1;
        unsigned int HasSubscriptions : 1;
        unsigned int Variant : 6;
        unsigned int VariantPayloadKind : 2;
    };
    unsigned int VariantPayload;
} RTL_FEATURE_CONFIGURATION, * PRTL_FEATURE_CONFIGURATION;

typedef struct _WNF_STATE_NAME
{
    unsigned long Data[2];
} WNF_STATE_NAME, * PWNF_STATE_NAME;

typedef struct _WNF_TYPE_ID
{
    struct _GUID TypeId;
} WNF_TYPE_ID, * PWNF_TYPE_ID;

typedef NTSTATUS(NTAPI* PWNF_USER_CALLBACK)(
    _In_ WNF_STATE_NAME StateName,
    _In_ DWORD ChangeStamp,
    _In_ PWNF_TYPE_ID TypeId,
    _In_opt_ PVOID CallbackContext,
    _In_ PVOID Buffer,
    _In_ ULONG BufferSize
    );

extern "C" __declspec(dllimport) NTSTATUS NTAPI RtlQueryFeatureConfiguration(_In_ ULONG FeatureId,
    _In_ RTL_FEATURE_CONFIGURATION_TYPE FeatureType,
    _Inout_ PULONGLONG ChangeStamp,
    _In_ PRTL_FEATURE_CONFIGURATION FeatureConfiguration
);

extern "C" __declspec(dllimport) NTSTATUS NTAPI RtlSubscribeWnfStateChangeNotification(
    _Outptr_ PVOID * SubscriptionHandle,
    _In_ WNF_STATE_NAME StateName,
    _In_ DWORD ChangeStamp,
    _In_ PWNF_USER_CALLBACK Callback,
    _In_opt_ PVOID CallbackContext,
    _In_opt_ PWNF_TYPE_ID TypeId,
    _In_opt_ ULONG SerializationGroup,
    _Reserved_ ULONG Flags
);

extern "C" __declspec(dllimport) NTSTATUS NTAPI RtlUnsubscribeWnfStateChangeNotification(
    _In_ PVOID SubscriptionHandle
);
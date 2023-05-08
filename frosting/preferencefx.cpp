#include "pch.h"
#include "preferencefx.h"

DWORD RegDWORDPrefGet(LPCWSTR lpValue, DWORD dwFallback)
{
    DWORD dwSize = sizeof(DWORD);
    DWORD dwVal = dwFallback;
    RegGetValueW(
        HKEY_CURRENT_USER,
        L"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced",
        lpValue,
        RRF_RT_REG_DWORD,
        NULL,
        &dwVal,
        &dwSize);
    return dwVal;
}

template <typename T>
bool PrefUpdate(LPCWSTR lpPrefName, T* pPref, T fallback) {
    auto newPref = (T)RegDWORDPrefGet(lpPrefName, fallback);
    if (newPref == *pPref)
        return false;

    *pPref = newPref;
    return true;
}

// I love C++
template bool PrefUpdate<bool>(LPCWSTR lpPrefName, bool* pPref, bool fallback);
template bool PrefUpdate<int>(LPCWSTR lpPrefName, int* pPref, int fallback);

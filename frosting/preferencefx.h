#pragma once
DWORD RegDWORDPrefGet(LPCWSTR lpValue, DWORD dwFallback);

template <typename T>
bool PrefUpdate(LPCWSTR lpPrefName, T* pPref, T fallback);
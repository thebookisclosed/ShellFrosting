#include "pch.h"
#include <cstdint>
#include "memfind.h"

uint8_t* Find64BInMemory(uint8_t* startAddress, uint32_t length, uint64_t pattern, int fixup)
{
    auto firstPatternByte = (uint8_t)pattern;
    for (auto i = 0u; i < length; i++)
    {
        auto offsetPtr = startAddress + i;
        if (*offsetPtr == firstPatternByte && *(uint64_t*)offsetPtr == pattern)
            return offsetPtr + fixup;
    }
    return NULL;
}

uint8_t* FindMasked128BInMemory(
    uint8_t* startAddress, uint32_t length,
    uint64_t hiPattern, uint64_t loPattern,
    uint64_t hiMask, uint64_t loMask, int fixup)
{
    auto firstPatternByte = (uint8_t)hiPattern;
    for (auto i = 0u; i < length; i++)
    {
        auto offsetPtr = startAddress + i;
        if (*offsetPtr == firstPatternByte)
        {
            auto offsetPtr64 = (uint64_t*)offsetPtr;
            if ((*offsetPtr64 & hiMask) == hiPattern && (*(offsetPtr64 + 1) & loMask) == loPattern)
                return offsetPtr + fixup;
        }

    }
    return NULL;
}
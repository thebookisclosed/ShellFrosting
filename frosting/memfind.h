#pragma once
uint8_t* Find64BInMemory(uint8_t* startAddress, uint32_t length, uint64_t pattern, int fixup);

uint8_t* FindMasked128BInMemory(
    uint8_t* startAddress, uint32_t length,
    uint64_t hiPattern, uint64_t loPattern,
    uint64_t hiMask, uint64_t loMask, int fixup);
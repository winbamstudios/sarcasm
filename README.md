# sarcasm
Assembler for the MD8 architecture written in C#

## Compilation
`sarcasm <input file> <output file>`

## Syntax
Use test.asmd as reference. ISA has been updated to fix JMP, JZ, and JNZ to use LBLs.

Follows instruction set specified on md8emu. This is an example script released before JZ and JNZ were fixed to use LBLs.

# sarcasm
Assembler for the MD8 architecture written in C#

## Compilation
`sarcasm <input file> <output file>`

## Syntax
`MOV,5,A,0

MOV,10,B,0

ADD,A,B,C

SUB,C,A,D

PUSH,D,0,0

MOV,0,A,0

POP,A,0,0

MOV,A,B,0

ADD,A,B,C

MOV,0,D,0

JZ,16,0,0

MOV,1,D,0

JNZ,20,0,0

NOP,0,0,0

NOP,0,0,0

HLT,0,0,0

MOV,99,A,0

HLT,0,0,0`

Follows instruction set specified on md8emu. This is an example script released before JZ and JNZ were fixed to use LBLs.

using System;
using System.IO;

namespace SARCASM
{
    internal class Program
    {
        public static string[] FileAsm;
        public static byte[] CompiledAsm = new byte[1024];
        public static int CurrentByte = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Although Really Cool ASseMbler Jan 2026 Release");
            Console.WriteLine("© 2026 winbamstudios");
            try
            {
                FileAsm = File.ReadAllLines(args[0]);
                Assembler.Assemble();
            }
            catch
            {
                Console.WriteLine("That's not a file.");
                Environment.Exit(1);
            }
            Console.WriteLine(System.Text.Encoding.Default.GetString(CompiledAsm));
            try
            {
                File.WriteAllBytes(args[1], CompiledAsm);
            }
            catch
            {
                Console.WriteLine("No output file specified.");
            }
        }
    }
    /*
    ID, Name, Description
    0 NOP (no operation)
    1 ADD R1,R2,R3 (add two registers together to output register)
    2 SUB R1,R2,R3 (subtract two registers to output register)
    3 MOV R1,R2 (copy content of register to another register)
    4 MOV R1,RAM (copy content of register to poInt32 in memory)
    5 MOV RAM,R1 (load content of byte specified into register)
    6 MOV INT,R1 (move integer into register)
    7 PUSH R1 (pushes content of register into "stack")
    8 POP R1 (pulls top of stack into register)
    9 HLT (halts)
    10 Not Implemented
    11 JMP ADDR (jumps to address) (optional)
    12 JZ ADDR (jumps to address if Register D zero)
    13 JNZ ADDR (jumps to address if Register D nonzero)

    instructions are 4 bytes each
    */
    public static class Assembler
    {
        public static void Assemble()
        {
            int e = -1;
            foreach (string s in Program.FileAsm)
            {
                e++;
                string[] syntax = s.Split(',');
                byte[] opcodes = new byte[4];
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        if (syntax[i].ToLower() == "mov")
                        {
                            if (syntax[i + 1].ToLower() == "a" || syntax[i + 1].ToLower() == "b" || syntax[i + 1].ToLower() == "c" || syntax[i + 1].ToLower() == "d")
                            {
                                if (syntax[i + 2].ToLower() == "a" || syntax[i + 1].ToLower() == "b" || syntax[i + 1].ToLower() == "c" || syntax[i + 1].ToLower() == "d")
                                {
                                    opcodes[i] = 3;
                                }
                                else
                                {
                                    opcodes[i] = 4;
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR! Line " + e+1 + " may be Integer to Register or RAM to Register! Which is it? (Y for Int2Reg, any other key for Ram2Reg)");
                                ConsoleKeyInfo cki = Console.ReadKey();
                                if (cki.KeyChar == 'y')
                                {
                                    opcodes[i] = 6;
                                }
                                else
                                {
                                    opcodes[i] = 5;
                                }
                            }
                        }
                        else if (syntax[i].ToLower() == "nop")
                        {
                            opcodes[i] = 0;
                        }
                        else if (syntax[i].ToLower() == "add")
                        {
                            opcodes[i] = 1;
                        }
                        else if (syntax[i].ToLower() == "sub")
                        {
                            opcodes[i] = 2;
                        }
                        else if (syntax[i].ToLower() == "push")
                        {
                            opcodes[i] = 7;
                        }
                        else if (syntax[i].ToLower() == "pop")
                        {
                            opcodes[i] = 8;
                        }
                        else if (syntax[i].ToLower() == "hlt")
                        {
                            opcodes[i] = 9;
                        }
                        else if (syntax[i].ToLower() == "lbl")
                        {
                            opcodes[i] = 10;
                        }
                        else if (syntax[i].ToLower() == "jmp")
                        {
                            opcodes[i] = 11;
                        }
                        else if (syntax[i].ToLower() == "jz")
                        {
                            opcodes[i] = 12;
                        }
                        else if (syntax[i].ToLower() == "jnz")
                        {
                            opcodes[i] = 13;
                        }
                        else if (syntax[i].ToLower() == "mba")
                        {
                            opcodes[i] = 14;
                        }
                        else if (syntax[i].ToLower() == "mbb")
                        {
                            opcodes[i] = 15;
                        }
                        else
                        {
                            Console.WriteLine("Instruction \"" + syntax[i] + "\" does not exist!");
                            Environment.Exit(1);
                        }
                    }
                    else if (i == 1)
                    {
                        if (syntax[i].ToLower() == "a")
                        {
                            opcodes[i] = 251;
                        }
                        else if (syntax[i].ToLower() == "b")
                        {
                            opcodes[i] = 252;
                        }
                        else if (syntax[i].ToLower() == "c")
                        {
                            opcodes[i] = 253;
                        }
                        else if (syntax[i].ToLower() == "d")
                        {
                            opcodes[i] = 254;
                        }
                        else
                        {
                            opcodes[i] = Convert.ToByte(syntax[i]);
                        }
                    }
                    else if (i == 2)
                    {
                        if (syntax[i].ToLower() == "a")
                        {
                            opcodes[i] = 251;
                        }
                        else if (syntax[i].ToLower() == "b")
                        {
                            opcodes[i] = 252;
                        }
                        else if (syntax[i].ToLower() == "c")
                        {
                            opcodes[i] = 253;
                        }
                        else if (syntax[i].ToLower() == "d")
                        {
                            opcodes[i] = 254;
                        }
                        else
                        {
                            opcodes[i] = Convert.ToByte(syntax[i]);
                        }
                    }
                    else if (i == 3)
                    {
                        if (syntax[i].ToLower() == "a")
                        {
                            opcodes[i] = 251;
                        }
                        else if (syntax[i].ToLower() == "b")
                        {
                            opcodes[i] = 252;
                        }
                        else if (syntax[i].ToLower() == "c")
                        {
                            opcodes[i] = 253;
                        }
                        else if (syntax[i].ToLower() == "d")
                        {
                            opcodes[i] = 254;
                        }
                        else
                        {
                            opcodes[i] = Convert.ToByte(syntax[i]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("I don't know what the heck you did, but you horribly screwed up the compiler. Good job");
                    }
                    
                }
                opcodes.CopyTo(Program.CompiledAsm, Program.CurrentByte);
                Program.CurrentByte += 4;
            }
        }
    }
}
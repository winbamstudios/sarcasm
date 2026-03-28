using System;
using System.IO;

namespace SARCASM
{
    internal class Program
    {
        public static string[] FileAsm;
        public static byte[] CompiledAsm = new byte[8192];
        public static int CurrentByte = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Simple Although Really Cool ASseMbler Mar 2026 Release");
            Console.WriteLine("© 2026 winbamstudios");
            try
            {
                FileAsm = File.ReadAllLines(args[0]);
                Assembler.Assemble();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //Environment.Exit(1);
            }
            Console.WriteLine(System.Text.Encoding.Default.GetString(CompiledAsm));
            try
            {
                File.WriteAllBytes(args[1], CompiledAsm);
            }
            catch (Exception ex)
            {
                //Console.WriteLine("No output file specified.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("(this likely means you did not specify an output file)");
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
                if (s.StartsWith(';') || s == "")
                {
                    continue;
                }
                string[] syntax = new string[4];
                string[] syntaxSpaceSplit = s.Split(' ');
                string syntaxInstruction = syntaxSpaceSplit[0];
                string[] syntaxArguments;
                string[] syntaxArgumentsButBetter = new string[3];
                if (syntaxSpaceSplit.Length > 1)
                {
                    syntaxArguments = syntaxSpaceSplit[1].Split(',');
                    for (int i = 0; i < 3; i++)
                    {
                        try
                        {
                            syntaxArgumentsButBetter[i] = syntaxArguments[i];
                        }
                        catch
                        {
                            syntaxArgumentsButBetter[i] = "0";
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        syntaxArgumentsButBetter[i] = "0";
                    }
                }
                syntax[0] = syntaxInstruction;
                syntax[1] = syntaxArgumentsButBetter[0];
                syntax[2] = syntaxArgumentsButBetter[1];
                syntax[3] = syntaxArgumentsButBetter[2];
                byte[] opcodes = new byte[4];
                bool isRam = false;
                bool keepAnEyeOnThisOne = false;
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        if (syntax[i].ToLower() == "mov")
                        {
                            if (syntax[i + 1].ToLower() == "a" || syntax[i + 1].ToLower() == "b" || syntax[i + 1].ToLower() == "c" || syntax[i + 1].ToLower() == "d" || syntax[i + 1].ToLower() == "sp")
                            {
                                if (syntax[i + 2].ToLower() == "a" || syntax[i + 1].ToLower() == "b" || syntax[i + 1].ToLower() == "c" || syntax[i + 1].ToLower() == "d" || syntax[i + 1].ToLower() == "sp")
                                {
                                    isRam = false;
                                    opcodes[i] = 3;
                                }
                                else
                                {
                                    isRam = true;
                                    opcodes[i] = 4;
                                }
                            }
                            else
                            {
                                if (syntax[i+1].StartsWith("$") || syntax[i].StartsWith("$"))
                                {
                                    isRam = true;
                                    opcodes[i] = 5;
                                }
                                else
                                {
                                    isRam = false;
                                    opcodes[i] = 6;
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
                        /*
                        else if (syntax[i].ToLower() == "mba")
                        {
                            if (syntax[i+1].StartsWith('R'))
                            {
                                opcodes[i] = 16;
                            }
                            else
                            {
                                opcodes[i] = 14;
                            }
                        }
                        else if (syntax[i].ToLower() == "mbb")
                        {
                            if (syntax[i+1].StartsWith('R'))
                            {
                                opcodes[i] = 17;
                            }
                            else
                            {
                                opcodes[i] = 15;
                            }
                        }
                        */
                        else if (syntax[i].ToLower() == "swb")
                        {
                            opcodes[i] = 14;
                        }
                        else if (syntax[i].ToLower() == "msg")
                        {
                            opcodes[i] = 15;
                        }
                        else if (syntax[i].ToLower() == "mul")
                        {
                            opcodes[i] = 16;
                        }
                        else if (syntax[i].ToLower() == "div")
                        {
                            opcodes[i] = 17;
                        }
                        else if (syntax[i].ToLower() == "mod")
                        {
                            opcodes[i] = 18;
                        }
                        else if (syntax[i].ToLower() == "jeq")
                        {
                            opcodes[i] = 19;
                        }
                        else if (syntax[i].ToLower() == "jlt")
                        {
                            opcodes[i] = 20;
                        }
                        else if (syntax[i].ToLower() == "jgt")
                        {
                            opcodes[i] = 21;
                        }
                        else if (syntax[i].ToLower() == "inb")
                        {
                            opcodes[i] = 22;
                        }
                        else if (syntax[i].ToLower() == "outb")
                        {
                            opcodes[i] = 23;
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
                        else if (syntax[i].ToLower() == "sp")
                        {
                            opcodes[i] = 255;
                        }
                        else if (syntax[i].StartsWith("$"))
                        {
                            opcodes[i] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[1];
                            opcodes[i + 1] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[0];
                        }
                        else
                        {
                            if (isRam)
                            {
                                opcodes[i] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[1];
                                opcodes[i + 1] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[0];
                            }
                            else
                            {
                                if (!syntax[i].StartsWith("$"))
                                {
                                    opcodes[i] = Convert.ToByte(syntax[i]);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    else if (i == 2)
                    {
                        if (syntax[i].ToLower() == "a")
                        {
                            if (!isRam)
                            {
                                opcodes[i] = 251;
                                keepAnEyeOnThisOne = false;
                            }
                            else
                            {
                                keepAnEyeOnThisOne = true;
                                continue;
                            }
                        }
                        else if (syntax[i].ToLower() == "b")
                        {
                            if (!isRam)
                            {
                                opcodes[i] = 252;
                                keepAnEyeOnThisOne = false;
                            }
                            else
                            {
                                keepAnEyeOnThisOne = true;
                                continue;
                            }
                        }
                        else if (syntax[i].ToLower() == "c")
                        {
                            if (!isRam)
                            {
                                opcodes[i] = 253;
                                keepAnEyeOnThisOne = false;
                            }
                            else
                            {
                                keepAnEyeOnThisOne = true;
                                continue;
                            }
                        }
                        else if (syntax[i].ToLower() == "d")
                        {
                            if (!isRam)
                            {
                                opcodes[i] = 254;
                                keepAnEyeOnThisOne = false;
                            }
                            else
                            {
                                keepAnEyeOnThisOne = true;
                                continue;
                            }
                        }
                        else if (syntax[i].ToLower() == "sp")
                        {
                            if (!isRam)
                            {
                                opcodes[i] = 255;
                                keepAnEyeOnThisOne = false;
                            }
                            else
                            {
                                keepAnEyeOnThisOne = true;
                                continue;
                            }
                        }
                        else if (syntax[i].StartsWith("$"))
                        {
                            opcodes[i] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[1];
                            opcodes[i + 1] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[0];
                        }
                        else
                        {
                            if (isRam)
                            {
                                opcodes[i] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[1];
                                opcodes[i + 1] = BitConverter.GetBytes(Convert.ToInt16(int.Parse(syntax[i].Substring(1), System.Globalization.NumberStyles.HexNumber)))[0];
                            }
                            else
                            {
                                if (!syntax[i - 1].StartsWith("$"))
                                {
                                    opcodes[i] = Convert.ToByte(syntax[i]);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    else if (i == 3)
                    {
                        if (keepAnEyeOnThisOne)
                        {
                            if (syntax[i - 1].ToLower() == "a")
                            {
                                opcodes[i] = 251;
                            }
                            else if (syntax[i - 1].ToLower() == "b")
                            {
                                opcodes[i] = 252;
                            }
                            else if (syntax[i - 1].ToLower() == "c")
                            {
                                opcodes[i] = 253;
                            }
                            else if (syntax[i - 1].ToLower() == "d")
                            {
                                opcodes[i] = 254;
                            }
                            else if (syntax[i - 1].ToLower() == "sp")
                            {
                                opcodes[i] = 255;
                            }
                            else
                            {
                                if (!syntax[i - 1].StartsWith("$"))
                                {
                                    opcodes[i] = Convert.ToByte(syntax[i]);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
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
                            else if (syntax[i].ToLower() == "sp")
                            {
                                opcodes[i] = 255;
                            }
                            else
                            {
                                if (!syntax[i - 1].StartsWith("$"))
                                {
                                    opcodes[i] = Convert.ToByte(syntax[i]);
                                }
                                else
                                {
                                    continue;
                                }
                            }
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
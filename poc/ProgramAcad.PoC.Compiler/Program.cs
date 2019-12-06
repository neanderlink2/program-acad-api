using ProgramAcad.Domain.Entities;
using ProgramAcad.Services.Modules.Compiling;
using ProgramAcad.Services.Modules.Compiling.Models;
using System;
using System.Threading.Tasks;

namespace ProgramAcad.PoC.Compiler
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Começando a compilação dos códigos...");
//            var client = new CompilerApiClient();

//            var csharp = await CompilarCSharp(client);
//            var java = await CompilarJava(client);
//            var python = await CompilarPython(client);
            
//            Console.WriteLine("E os resultados das compilações são: ");

//            Console.WriteLine(@$"
//C# : Tempo de CPU - {csharp.CpuTime}s
//Saída: {csharp.Output}");

//            Console.WriteLine("-----------------------------------------------------");
//            Console.WriteLine(@$"
//Java : Tempo de CPU - {java.CpuTime}s
//Saída: {java.Output}");

//            Console.WriteLine("-----------------------------------------------------");
//            Console.WriteLine(@$"
//Python : Tempo de CPU - {python.CpuTime}s
//Saída: {python.Output}");

            Console.ReadKey();
        }

        private static async Task<CompilerResponse> CompilarCSharp(CompilerApiClient client)
        {
            return await client.Compile(@"
                using System;
                public class Program {
                    public static void Main(string[] args) {
                        Console.WriteLine(""Ola mundo"");
                        for (int i = 0; i < 100; i++)
                        {
                            Console.Write(i + "", "");
                        }
                    }
                }", new string[] {  }, LinguagensProgramacao.CSharp);
        }

        private static async Task<CompilerResponse> CompilarJava(CompilerApiClient client)
        {
            return await client.Compile(@"                
                public class Main {
                    public static void main(String[] args) {   
                        System.out.println(""Ola mundo"");
                        for (int i = 0; i < 100; i++)
                        {
                            System.out.print(i + "", "");
                        }
                    }
                }", Array.Empty<string>(), LinguagensProgramacao.Java);
        }

        private static async Task<CompilerResponse> CompilarPython(CompilerApiClient client)
        {
            return await client.Compile(@"print('Ola mundo');
for x in range(100):
    print('{}, '.format(x), end = '');", new string[] {  }, LinguagensProgramacao.Python);
        }
    }
}

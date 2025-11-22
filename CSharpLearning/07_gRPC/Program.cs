using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gRPCExamples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== C# gRPC (Google Remote Procedure Call) ===\n");

            // ===== PRÍKLAD 1: Čo je gRPC? =====
            Console.WriteLine("--- PRÍKLAD 1: Úvod do gRPC ---");
            Priklad1_UvodDoGRPC();
            Console.WriteLine();

            // ===== PRÍKLAD 2: Protocol Buffers (Protobuf) =====
            Console.WriteLine("--- PRÍKLAD 2: Protocol Buffers ---");
            Priklad2_Protobuf();
            Console.WriteLine();

            // ===== PRÍKLAD 3: Typy gRPC služieb =====
            Console.WriteLine("--- PRÍKLAD 3: Typy gRPC služieb ---");
            Priklad3_TypySluzieb();
            Console.WriteLine();

            // ===== PRÍKLAD 4: Simulácia gRPC komunikácie =====
            Console.WriteLine("--- PRÍKLAD 4: Simulácia gRPC komunikácie ---");
            await Priklad4_SimulaciaGRPC();
            Console.WriteLine();

            // ===== PRÍKLAD 5: gRPC vs REST API =====
            Console.WriteLine("--- PRÍKLAD 5: gRPC vs REST API ---");
            Priklad5_GRPCvsREST();
            Console.WriteLine();

            Console.WriteLine("\nStlač ENTER pre ukončenie...");
            Console.ReadLine();
        }

        // ===== PRÍKLAD 1: Úvod do gRPC =====
        static void Priklad1_UvodDoGRPC()
        {
            Console.WriteLine("gRPC je moderný RPC (Remote Procedure Call) framework od Google.\n");
            
            Console.WriteLine("Kľúčové vlastnosti:");
            Console.WriteLine("• Vysoký výkon - používa HTTP/2 a binárnu serializáciu");
            Console.WriteLine("• Multi-platformový - C#, Java, Python, Go, atď.");
            Console.WriteLine("• Strongly typed - definované cez Protocol Buffers");
            Console.WriteLine("• Bidirectional streaming - real-time komunikácia");
            Console.WriteLine("• Built-in authentication, load balancing, atď.\n");

            Console.WriteLine("Použitie:");
            Console.WriteLine("• Mikroslužby (microservices)");
            Console.WriteLine("• Real-time aplikácie");
            Console.WriteLine("• Mobile-backend komunikácia");
            Console.WriteLine("• IoT systémy");
        }

        // ===== PRÍKLAD 2: Protocol Buffers =====
        static void Priklad2_Protobuf()
        {
            Console.WriteLine("Protocol Buffers (protobuf) je jazyk pre definíciu dátových štruktúr.\n");

            Console.WriteLine("Príklad .proto súboru:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("syntax = \"proto3\";");
            Console.WriteLine();
            Console.WriteLine("service UzivatelService {");
            Console.WriteLine("  rpc GetUzivatel (UzivatelRequest) returns (UzivatelResponse);");
            Console.WriteLine("  rpc CreateUzivatel (CreateUzivatelRequest) returns (UzivatelResponse);");
            Console.WriteLine("}");
            Console.WriteLine();
            Console.WriteLine("message UzivatelRequest {");
            Console.WriteLine("  int32 id = 1;");
            Console.WriteLine("}");
            Console.WriteLine();
            Console.WriteLine("message UzivatelResponse {");
            Console.WriteLine("  int32 id = 1;");
            Console.WriteLine("  string meno = 2;");
            Console.WriteLine("  string email = 3;");
            Console.WriteLine("}");
            Console.WriteLine("----------------------------------------\n");

            Console.WriteLine("Z .proto súboru sa automaticky vygeneruje C# kód:");
            
            // Simulácia vygenerovanej triedy
            var uzivatel = new UzivatelProtoSimulacia
            {
                Id = 1,
                Meno = "Peter Novák",
                Email = "peter@example.com"
            };

            Console.WriteLine($"\nVygenerovaná C# trieda:");
            Console.WriteLine($"  ID: {uzivatel.Id}");
            Console.WriteLine($"  Meno: {uzivatel.Meno}");
            Console.WriteLine($"  Email: {uzivatel.Email}");
        }

        // ===== PRÍKLAD 3: Typy gRPC služieb =====
        static void Priklad3_TypySluzieb()
        {
            Console.WriteLine("gRPC podporuje 4 typy komunikácie:\n");

            Console.WriteLine("1. UNARY RPC (request-response)");
            Console.WriteLine("   • Klient pošle 1 požiadavku → Server odpovedá 1 odpoveďou");
            Console.WriteLine("   • Podobné ako klasické REST API");
            Console.WriteLine("   rpc GetUzivatel(Request) returns (Response);");
            Console.WriteLine();

            Console.WriteLine("2. SERVER STREAMING RPC");
            Console.WriteLine("   • Klient pošle 1 požiadavku → Server posiela stream odpovedí");
            Console.WriteLine("   • Napr. sťahovanie veľkého súboru, real-time updates");
            Console.WriteLine("   rpc ListUzivatelia(Request) returns (stream Response);");
            Console.WriteLine();

            Console.WriteLine("3. CLIENT STREAMING RPC");
            Console.WriteLine("   • Klient posiela stream požiadaviek → Server odpovie 1 odpoveďou");
            Console.WriteLine("   • Napr. upload veľkého súboru");
            Console.WriteLine("   rpc UploadData(stream Request) returns (Response);");
            Console.WriteLine();

            Console.WriteLine("4. BIDIRECTIONAL STREAMING RPC");
            Console.WriteLine("   • Klient a server si vymieňajú streamy (obojsmerná komunikácia)");
            Console.WriteLine("   • Napr. chat aplikácia, online hra");
            Console.WriteLine("   rpc Chat(stream Message) returns (stream Message);");
        }

        // ===== PRÍKLAD 4: Simulácia gRPC komunikácie =====
        static async Task Priklad4_SimulaciaGRPC()
        {
            Console.WriteLine("Simulácia gRPC server-client komunikácie:\n");

            // Simulovaný gRPC server
            var server = new SimulovanyGRPCServer();
            
            // Simulovaný klient
            Console.WriteLine("=== UNARY RPC ===");
            var uzivatel = await server.GetUzivatelAsync(1);
            Console.WriteLine($"Získaný užívateľ: {uzivatel.Meno} ({uzivatel.Email})");
            Console.WriteLine();

            Console.WriteLine("=== SERVER STREAMING ===");
            await foreach (var u in server.GetVsetkychUzivatelovAsync())
            {
                Console.WriteLine($"  Stream -> {u.Id}: {u.Meno}");
                await Task.Delay(300); // Simulácia stream delay
            }
            Console.WriteLine();

            Console.WriteLine("=== CLIENT STREAMING ===");
            var spravy = new List<string> { "Správa 1", "Správa 2", "Správa 3" };
            var vysledok = await server.PosliViacespraAsync(spravy);
            Console.WriteLine($"Server prijal {vysledok} správ");
        }

        // ===== PRÍKLAD 5: gRPC vs REST =====
        static void Priklad5_GRPCvsREST()
        {
            Console.WriteLine("Porovnanie gRPC a REST API:\n");

            Console.WriteLine("╔══════════════════╦═══════════════════╦═══════════════════╗");
            Console.WriteLine("║ Vlastnosť        ║ gRPC              ║ REST API          ║");
            Console.WriteLine("╠══════════════════╬═══════════════════╬═══════════════════╣");
            Console.WriteLine("║ Protokol         ║ HTTP/2            ║ HTTP/1.1          ║");
            Console.WriteLine("║ Formát dát       ║ Protobuf (binár)  ║ JSON (text)       ║");
            Console.WriteLine("║ Výkon            ║ Vysoký            ║ Nižší             ║");
            Console.WriteLine("║ Streaming        ║ Áno (native)      ║ Obmedzené         ║");
            Console.WriteLine("║ Browser podpora  ║ Obmedzená         ║ Plná              ║");
            Console.WriteLine("║ Čitateľnosť      ║ Binárna           ║ Ľahko čitateľná   ║");
            Console.WriteLine("║ Code generation  ║ Automatická       ║ Manuálna/tooling  ║");
            Console.WriteLine("╚══════════════════╩═══════════════════╩═══════════════════╝");

            Console.WriteLine("\nKedy použiť gRPC:");
            Console.WriteLine("✓ Mikroslužby s vysokými požiadavkami na výkon");
            Console.WriteLine("✓ Real-time aplikácie (streaming)");
            Console.WriteLine("✓ Polyglot systémy (rôzne jazyky)");
            Console.WriteLine("✓ Interne medzi službami");

            Console.WriteLine("\nKedy použiť REST:");
            Console.WriteLine("✓ Verejné API pre webové aplikácie");
            Console.WriteLine("✓ Potreba širokej kompatibility");
            Console.WriteLine("✓ Jednoduché CRUD operácie");
            Console.WriteLine("✓ Developer friendly, ľahko debugovateľné");
        }
    }

    // ===== Simulované triedy =====
    
    // Simulácia vygenerovanej Protocol Buffer triedy
    public class UzivatelProtoSimulacia
    {
        public int Id { get; set; }
        public string Meno { get; set; }
        public string Email { get; set; }
    }

    // Simulovaný gRPC server
    public class SimulovanyGRPCServer
    {
        private readonly List<UzivatelProtoSimulacia> databaza = new()
        {
            new() { Id = 1, Meno = "Peter Novák", Email = "peter@example.com" },
            new() { Id = 2, Meno = "Jana Kováčová", Email = "jana@example.com" },
            new() { Id = 3, Meno = "Martin Svoboda", Email = "martin@example.com" }
        };

        // Unary RPC
        public async Task<UzivatelProtoSimulacia> GetUzivatelAsync(int id)
        {
            await Task.Delay(100); // Simulácia network delay
            return databaza.Find(u => u.Id == id);
        }

        // Server Streaming RPC
        public async IAsyncEnumerable<UzivatelProtoSimulacia> GetVsetkychUzivatelovAsync()
        {
            foreach (var uzivatel in databaza)
            {
                await Task.Delay(200); // Simulácia streaming
                yield return uzivatel;
            }
        }

        // Client Streaming RPC
        public async Task<int> PosliViacespraAsync(List<string> spravy)
        {
            await Task.Delay(100);
            return spravy.Count;
        }
    }

    // ===== Príklad štruktúry gRPC projektu =====
    
    /* 
    V reálnom gRPC projekte by ste mali:

    1. .proto súbor (napr. uzivatel.proto):
       syntax = "proto3";
       service UzivatelService {
         rpc GetUzivatel (UzivatelRequest) returns (UzivatelResponse);
       }

    2. .csproj s Grpc.AspNetCore balíčkom:
       <ItemGroup>
         <Protobuf Include="Protos\uzivatel.proto" GrpcServices="Server" />
       </ItemGroup>

    3. Server implementácia:
       public class UzivatelServiceImpl : UzivatelService.UzivatelServiceBase
       {
         public override Task<UzivatelResponse> GetUzivatel(
             UzivatelRequest request, ServerCallContext context)
         {
           // Implementácia
         }
       }

    4. Client:
       var channel = GrpcChannel.ForAddress("https://localhost:5001");
       var client = new UzivatelService.UzivatelServiceClient(channel);
       var response = await client.GetUzivatelAsync(new UzivatelRequest { Id = 1 });

    5. Konfigurácia servera (Program.cs):
       builder.Services.AddGrpc();
       app.MapGrpcService<UzivatelServiceImpl>();
    */
}

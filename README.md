# First MCP Server

A small, beginner-friendly Model Context Protocol (MCP) server built with .NET. It exposes one tool that greets a person by name. The server communicates over standard input and output (STDIO), which makes it suitable for local MCP clients and for trying with MCP Inspector.

## What it does

The server registers MCP services, uses the STDIO transport, and discovers tool methods from the project assembly. Its only tool is:

| Tool | Input | Result |
| --- | --- | --- |
| `GreetTheUser` | `name` (string) | A greeting: `Hello <name>! Welcome to your first MCP server.` |

For example, calling `GreetTheUser` with `name` set to `Ada` returns:

```text
Hello Ada! Welcome to your first MCP server.
```

## Prerequisites

- [.NET 11 SDK](https://dotnet.microsoft.com/download/dotnet/11.0)
- Node.js and npm, if you want to run MCP Inspector through `npx`

## Build and run

From this directory, restore and build the project:

```bash
dotnet restore
dotnet build
```

Run the server with:

```bash
dotnet run
```

The process waits for an MCP client to communicate over STDIO. This is expected: it is not a web server and does not expose an HTTP page or endpoint. When connecting through an MCP client, that client starts the server process and exchanges protocol messages through its standard input and output.

## Try it with MCP Inspector

You do not need to launch `dotnet run` separately before opening Inspector. Configure Inspector to start the project as its STDIO child process. From the project root, run:

```bash
npx @modelcontextprotocol/inspector
```

Inspector should connect to the server and show the `GreetTheUser` tool. Open the tool, provide a `name` value such as `Ada`, and run it to see the greeting.

If you prefer the Inspector UI's connection form, choose **STDIO** and enter:

- **Command:** `dotnet`
- **Arguments:** `run`
- **Working directory:** the `FirstMCPServer` project directory

Keep the Inspector process attached to the terminal so you can see diagnostics and stop it with `Ctrl+C` when finished. MCP protocol output uses standard output; avoid adding ordinary console messages there, since they can interfere with STDIO communication.

## Project layout

```text
FirstMCPServer/
├── FirstMCPServer.csproj  # .NET target and package references
├── Program.cs             # Host setup, MCP registration, and STDIO transport
└── GreetingTools.cs       # The GreetTheUser tool
```

## How it is put together

- `Program.cs` creates a .NET Generic Host, registers the MCP server, selects STDIO transport, and discovers tools from the assembly with `WithToolsFromAssembly()`.
- `GreetingTools.cs` marks the static class as an MCP tool type and the greeting method as a tool. The description attribute supplies a description for MCP clients.
- `FirstMCPServer.csproj` targets `net11.0` and references `ModelContextProtocol` and `Microsoft.Extensions.Hosting`.

This project is a learning starting point. It currently implements a single greeting tool; it does not include resources, prompts, HTTP transport, or external service integrations.
namespace FirstMCPServer;

using ModelContextProtocol.Server;
using System.ComponentModel;

[McpServerToolType]
public static class GreetingTools
{
    [McpServerTool, Description("Greets a user by their name. Need to pass in the name and it will return a greeting message.")]
    public static string GreetTheUser(string name)
    {
        return $"Hello {name}! Welcome to your first MCP server.";
    }
}

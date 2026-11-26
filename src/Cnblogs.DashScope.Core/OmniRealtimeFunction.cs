namespace Cnblogs.DashScope.Core;

/// <summary>
/// A function the model of an omni realtime session may call. The model decides to call it by
/// its description, the client executes it and writes the result back with a function_call_output
/// item.
/// </summary>
/// <param name="Name">The name of the function, e.g. get_current_weather.</param>
/// <param name="Description">What the function does, the model picks the function by it.</param>
/// <param name="Parameters">The JSON schema of the arguments as an object, e.g.
/// <c>new { type = "object", properties = new { location = new { type = "string" } }, required = new[] { "location" } }</c>.
/// Unset when the function takes no arguments. Deserialized as a <see cref="System.Text.Json.JsonElement"/>
/// in the sessions of the server events.</param>
public record OmniRealtimeFunction(
    string? Name = null,
    string? Description = null,
    object? Parameters = null);

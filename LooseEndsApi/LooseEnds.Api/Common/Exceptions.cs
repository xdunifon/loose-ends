namespace LooseEnds.Api.Common;

public class NotFoundException(string message) : Exception(message) { }

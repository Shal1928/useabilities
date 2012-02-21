namespace Common.Code.UseAbilities.System.Core.Enums
{
    /// <summary>
    /// WorkSation: The operating system is Windows 7, Windows Vista, Windows XP Professional, Windows XP Home Edition, or Windows 2000 Professional.
    /// DomainController: The system is a domain controller and the operating system is Windows Server 2008 R2, Windows Server 2008, Windows Server 2003, or Windows 2000 Server.
    /// Server: The operating system is Windows Server 2008 R2, Windows Server 2008, Windows Server 2003, or Windows 2000 Server.
    /// Note that a server that is also a domain controller is reported as VER_NT_DOMAIN_CONTROLLER, not VER_NT_SERVER.
    /// </summary>
    internal enum ProductType
    {
        WorkStation = 0x0000001,
        DomainController = 0x0000002,
        Server = 0x0000003
    }
}

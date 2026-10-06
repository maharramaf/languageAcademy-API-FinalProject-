using Service.Helpers.DTOs.Contacts;


namespace Service.Services.Interfaces
{
    public interface IContactService
    {
        Task<ContactSectionDto?> GetUIAsync();
        Task<bool> SendAsync(ContactSendDto dto);
    }
}

using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Service.Helpers.DTOs.Messages;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;
        private readonly UserManager<AppUser> _userManager;

        public MessageService(
            IMessageRepository messageRepo,
            IEnrollmentRepository enrollmentRepo,
            UserManager<AppUser> userManager)
        {
            _messageRepo = messageRepo;
            _enrollmentRepo = enrollmentRepo;
            _userManager = userManager;
        }

        public async Task<IReadOnlyList<ConversationDto>> GetInboxAsync(string userId)
        {
            var items = await _messageRepo.GetInboxAsync(userId);
            return items
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Select(group =>
                {
                    var last = group.First();
                    var other = last.SenderId == userId ? last.Receiver : last.Sender;
                    return new ConversationDto
                    {
                        UserId = other.Id,
                        Name = DisplayName(other),
                        LastBody = last.Body,
                        LastAt = last.CreatedAt,
                        Unread = group.Count(m => m.ReceiverId == userId && !m.IsRead)
                    };
                })
                .OrderByDescending(m => m.LastAt)
                .ToList();
        }

        public async Task<IReadOnlyList<ContactDto>> GetContactsAsync(string userId)
        {
            var ids = await ContactIdsAsync(userId);
            var contacts = new List<ContactDto>();
            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null) continue;
                var roles = await _userManager.GetRolesAsync(user);
                contacts.Add(new ContactDto
                {
                    UserId = user.Id,
                    Name = DisplayName(user),
                    Role = roles.FirstOrDefault() ?? string.Empty
                });
            }

            return contacts.OrderBy(m => m.Name).ToList();
        }

        public async Task<ThreadDto?> GetThreadAsync(string userId, string otherId)
        {
            if (!await CanTalkAsync(userId, otherId))
                return null;

            var other = await _userManager.FindByIdAsync(otherId);
            if (other is null)
                return null;

            await _messageRepo.MarkReadAsync(userId, otherId);
            var items = await _messageRepo.GetThreadAsync(userId, otherId);
            return new ThreadDto
            {
                UserId = other.Id,
                Name = DisplayName(other),
                Messages = items.Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    SenderName = DisplayName(m.Sender),
                    Body = m.Body,
                    CreatedAt = m.CreatedAt,
                    Mine = m.SenderId == userId
                }).ToList()
            };
        }

        public async Task<MessageResultDto> SendAsync(string userId, MessageCreateDto dto)
        {
            var receiverId = (dto.ReceiverId ?? string.Empty).Trim();
            var body = (dto.Body ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(receiverId))
                return Fail("Choose someone to message.");
            if (body.Length is 0 or > 2000)
                return Fail("Write a message.");
            if (!await CanTalkAsync(userId, receiverId))
                return Fail("You cannot message this user.");

            await _messageRepo.AddAsync(new Message
            {
                SenderId = userId,
                ReceiverId = receiverId,
                Body = body
            });

            return new MessageResultDto { Succeeded = true };
        }

        private async Task<bool> CanTalkAsync(string userId, string otherId)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(otherId) || userId == otherId)
                return false;

            var ids = await ContactIdsAsync(userId);
            return ids.Contains(otherId);
        }

        private async Task<HashSet<string>> ContactIdsAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return new HashSet<string>();

            var roles = await _userManager.GetRolesAsync(user);
            var ids = new HashSet<string>();

            if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
            {
                foreach (var student in await _userManager.GetUsersInRoleAsync(Roles.Student))
                    ids.Add(student.Id);
                foreach (var teacher in await _userManager.GetUsersInRoleAsync(Roles.Teacher))
                    ids.Add(teacher.Id);
            }

            if (roles.Contains(Roles.Student))
            {
                foreach (var teacherId in await _enrollmentRepo.GetTeacherIdsByStudentAsync(userId))
                    ids.Add(teacherId);
            }

            if (roles.Contains(Roles.Teacher))
            {
                foreach (var studentId in await _enrollmentRepo.GetStudentIdsByTeacherAsync(userId))
                    ids.Add(studentId);
            }

            ids.Remove(userId);
            return ids;
        }

        private static string DisplayName(AppUser user)
        {
            var name = $"{user.Name} {user.Surname}".Trim();
            return string.IsNullOrWhiteSpace(name) ? (user.Email ?? "User") : name;
        }

        private static MessageResultDto Fail(string error)
        {
            return new MessageResultDto
            {
                Succeeded = false,
                Errors = new[] { error }
            };
        }
    }
}

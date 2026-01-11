namespace Solimus.Application.Users.DTO_s;

public record ChangeUserPasswordRequest(
    string NewPassword,
    string ConfirmNewPassword);
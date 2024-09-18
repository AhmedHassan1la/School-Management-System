﻿namespace SchoolManagmen.Contracts.User;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    IList<string> Roles
);
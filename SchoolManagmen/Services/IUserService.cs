﻿using SchoolManagmen.Contracts.User;

namespace SchoolManagmen.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Result<UserResponse>> GetAsync(string id);
        Task<Result<UserResponse>> AddAsync(CreateUserRequest request, CancellationToken cancellationToken = default);
        Task<Result> UpdateAsync(string id, UpdateUserRequest request, CancellationToken cancellationToken = default);

        Task<Result<UserProfileResponse>> GetProfileAsync(string userId);
        Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);
        Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);

        //Task<Result> ToggleStatus(string id);
        //Task<Result> Unlock(string id);
    }
}

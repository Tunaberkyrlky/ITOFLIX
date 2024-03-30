using ITOFLIX.DTO.Requests;
using ITOFLIX.DTO.Responses;
using ITOFLIX.Models;
using System.Collections.Generic;

namespace ITOFLIX.DTO.Converters
{
    public class UserConverter
    {
        public ITOFLIXUser Convert(UserCreateRequest request)
        {
            ITOFLIXUser newITOFLIXUser = new()
            {
                UserName = request.Email,

                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                BirthDate = request.BirthDate,

                Passive = false,

            };
            return newITOFLIXUser;
        }

        public UserGetResponse Convert(ITOFLIXUser iTOFLIXUser)
        {
            UserGetResponse newUserResponse = new()
            {
                Id = iTOFLIXUser.Id,
                UserName = iTOFLIXUser.UserName!,
                Name = iTOFLIXUser.Name,
                Email = iTOFLIXUser.Email!,
                PhoneNumber = iTOFLIXUser.PhoneNumber!,
                BirthDate = iTOFLIXUser.BirthDate,

                Passive = iTOFLIXUser.Passive,

            };
            return newUserResponse;
        }
        public List<UserGetResponse> Convert(List<ITOFLIXUser> iTOFLIXUsers)
        {
            List<UserGetResponse> usersResponses = new();
            foreach (var user in iTOFLIXUsers)
            {
                usersResponses.Add(Convert(user));
            }
            return usersResponses;
        }
    }
}

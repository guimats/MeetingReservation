using MeetingReservation.Application.Extensions.Mapping;
using MeetingReservation.Communication.Responses;
using MeetingReservation.Domain.Repositories.User;
using MeetingReservation.Exceptions;
using MeetingReservation.Exceptions.ExceptionsBase;

namespace MeetingReservation.Application.UseCases.User.GetById;

public class GetProfileByIdUseCase : IGetProfileByIdUseCase
{
    private readonly IUserReadOnlyRepository _userRepository;

    public GetProfileByIdUseCase(IUserReadOnlyRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<ResponseUserProfileJson> Execute(long id)
    {
        var user = await _userRepository.GetById(id);

        if (user is null)
            throw new NotFoundException(ResourceMessagesException.USER_NOT_FOUND);

        var response = user.MapToProfile();
        
        return response;
    }
}

using Application.DTOs;

namespace Application.Interfaces.Services;

public interface IRentalService
{
    Task<IEnumerable<RentalDto>> GetRentalsAsync(CancellationToken ct = default);
    Task<IEnumerable<RentalDto>> GetRentalsByUserIdAsync(int userId, CancellationToken ct = default);
    Task RentAsync(RentalDto rentalDto, CancellationToken ct = default);
    Task ReturnBookAsync(int rentId, CancellationToken ct = default);
    Task ExtendReturnDateAsync(ExtendDateDto extendDateDto, CancellationToken ct = default);
}
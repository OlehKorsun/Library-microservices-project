using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;

namespace Application.Services;

public class RentalService(IRentalRepository rentalRepository) : IRentalService
{
    public async Task<IEnumerable<RentalDto>> GetRentalsAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<RentalDto>> GetRentalsByUserIdAsync(int rentalId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task RentAsync(RentalDto rentalDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public async Task ReturnBookAsync(int rentId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
    
    public async Task ExtendReturnDateAsync(ExtendDateDto extendDateDto, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
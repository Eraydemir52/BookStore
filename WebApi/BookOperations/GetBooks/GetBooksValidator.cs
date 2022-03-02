using FluentValidation;

namespace WebaApi.BookOperations
{
    public class GetBooksValidator : AbstractValidator<GetBooksQuery>
    {
        public GetBooksValidator()
        {
            
        }
    }
}
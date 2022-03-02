using FluentValidation;

namespace WebaApi.BookOperations.GetBookDetail
{
    public class GetBookDetailValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailValidator()
        {
            RuleFor(query => query.BookId).GreaterThan(0);
        }
    }
}
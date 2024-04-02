using FluentValidation;
using FluentValidation.Results;
using Game.Application.Contracts.Core;

namespace Game.Application.Contracts
{
    public class BoardStatePostRequest : BaseRequest
    {
        public required BoardRequest Board { get; set; }

        public override ValidationResult Validate()
        {
            return new BoardStatePostRequestValidation().Validate(this);
        }
    }

    public class BoardStatePostRequestValidation : AbstractValidator<BoardStatePostRequest>
    {
        public BoardStatePostRequestValidation()
        {
            RuleFor(o => o.Board).NotNull().WithMessage("The board is required.");
            When(o => o.Board != null, () =>
            {
                RuleFor(o => o.Board.GridRequest.Width).InclusiveBetween(10, 100).WithMessage("The width must be between 10 and 100");
                RuleFor(o => o.Board.GridRequest.Height).InclusiveBetween(10, 100).WithMessage("The height must be between 10 and 100");
                
                RuleFor(o => o.Board.GridRequest.Cells).NotNull().NotEmpty().WithMessage("The board cells are required");
                RuleFor(o => o.Board.GridRequest.Cells.Length).InclusiveBetween(10, 100).WithMessage("The board cells must be between 10 and 100");

                RuleFor(o => o.Board)
                .Must(b => b.GridRequest.Height == b.GridRequest.Width)
                .WithMessage("The board must be a square between 10 and 100");
            });
        }
    }
}

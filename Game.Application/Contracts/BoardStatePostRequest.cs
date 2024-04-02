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
                RuleFor(o => o.Board.Grid.Width).InclusiveBetween(10, 100).WithMessage("The width must be between 10 and 100");
                RuleFor(o => o.Board.Grid.Height).InclusiveBetween(10, 100).WithMessage("The height must be between 10 and 100");
                
                RuleFor(o => o.Board.Grid.Cells).NotNull().NotEmpty().WithMessage("The board cells are required");
                RuleFor(o => o.Board.Grid.Cells.Length).InclusiveBetween(10, 100).WithMessage("The board cells must be between 10 and 100");

                RuleFor(o => o.Board)
                .Must(b => b.Grid.Height == b.Grid.Width)
                .WithMessage("The board must be a square between 10 and 100");
            });
        }
    }
}

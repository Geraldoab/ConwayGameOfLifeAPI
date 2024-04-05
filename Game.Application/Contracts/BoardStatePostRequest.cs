using FluentValidation;
using FluentValidation.Results;
using Game.Application.Contracts.Core;
using Game.Domain.Resources;
using System.Diagnostics.CodeAnalysis;

namespace Game.Application.Contracts
{
    public class BoardStatePostRequest : BaseRequest
    {
        public BoardStatePostRequest()
        {
        }

        [SetsRequiredMembers]
        public BoardStatePostRequest(BoardRequest board)
        {
            Board = board;
        }

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
            RuleFor(o => o.Board).NotNull().WithMessage(Messages.BOARD_IS_REQUIRED);
            When(o => o.Board != null, () =>
            {
                RuleFor(o => o.Board.Grid.Width).InclusiveBetween(10, 100).WithMessage(Messages.WIDTH_OUT_OF_RANGE);
                RuleFor(o => o.Board.Grid.Height).InclusiveBetween(10, 100).WithMessage(Messages.HEIGHT_OUT_OF_RANGE);
                
                RuleFor(o => o.Board.Grid.Cells).NotNull().NotEmpty().WithMessage(Messages.BOARD_CELLS_IS_REQUIRED);
                RuleFor(o => o.Board.Grid.Cells.Length).InclusiveBetween(10, 100).WithMessage(Messages.BOARD_CELLS_OUT_OF_RANGE);

                RuleFor(o => o.Board)
                .Must(b => b.Grid.Height == b.Grid.Width && b.Grid.Cells.GetLength(0) == b.Grid.Cells.GetLength(1))
                .WithMessage(Messages.BOARD_MUST_BE_A_SQUARE);
            });
        }
    }
}

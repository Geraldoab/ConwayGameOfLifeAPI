using FluentValidation;
using FluentValidation.Results;
using Game.Domain.Resources;

namespace Game.Domain.Model
{
    public class Grid
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int[,] Cells { get; set; } = null!;

        public ValidationResult Validate()
        {
            return new GridValidation().Validate(this);
        }
    }

    public class GridValidation : AbstractValidator<Grid>
    {
        public GridValidation() 
        {
            RuleFor(o => o.Width).InclusiveBetween(10, 100).WithMessage(Messages.WIDTH_OUT_OF_RANGE);
            RuleFor(o => o.Height).InclusiveBetween(10, 100).WithMessage(Messages.HEIGHT_OUT_OF_RANGE);

            RuleFor(o => o.Cells).NotNull().NotEmpty().WithMessage(Messages.BOARD_CELLS_IS_REQUIRED);
            RuleFor(o => o.Cells.Length).InclusiveBetween(10, 100).WithMessage(Messages.BOARD_CELLS_OUT_OF_RANGE);
        }
    }
}

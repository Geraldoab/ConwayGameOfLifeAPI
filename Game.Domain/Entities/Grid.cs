using FluentValidation;
using FluentValidation.Results;
using Game.Domain.Core;
using Game.Domain.Resources;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game.Domain.Entities
{
    public sealed class Grid : Entity
    {
        public int Width { get; set; }

        public int Height { get; set; }

        /// <summary>
        /// The 2D array represented into a comma-separated string
        /// </summary>
        public string? TwoDimensionalStringArray { get; set; }

        [NotMapped]
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

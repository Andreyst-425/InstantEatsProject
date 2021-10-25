using InstantEatService.Models;

namespace InstantEatService.Dto
{
    public class CategoryDto
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name;

        public CategoryDto(Category category)
        {
            Name = category.Name;
        }
    }
}

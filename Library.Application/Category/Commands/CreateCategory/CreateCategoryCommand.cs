using Library.Common.Models.Category;

namespace Library.Application.Category.Commands.CreateCategory;

public class CreateCategoryCommand : BaseNameItem, IRequest<GetCategoryModel>
{
}
using EFDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDAL.Configs
{
	/// <summary>
    /// Not necessary configuration class for "Category" entity that adds default data to the DB
    /// </summary>
    public class CategoryDefaultDataConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category[]
                {
                    new Category { Id = 1, Name = "Телефоны и аксессуары" },
                    new Category { Id = 2, Name = "Компьютеры и оргтехника" },
                    new Category { Id = 3, Name = "Электроника" },
                    new Category { Id = 4, Name = "Бытовая техника" },
                    new Category { Id = 5, Name = "Автотовары" },
                    new Category { Id = 6, Name = "Одежда для мужчин"},
                    new Category { Id = 7, Name = "Спорт и развлечения" },
                    new Category { Id = 8, Name = "Для дома и сада"}
                });
        }
    }
}
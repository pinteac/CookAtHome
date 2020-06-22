using cookathome.Tests.Mocks;
using cootathome.Services;
using cootathome.ViewModels;
using Moq;
using System;
using Xunit;

namespace cookathome.Tests
{
    public class AddCategoryTests
    {
        [Fact]
        public void All_objects_in_constructor_are_created()
        {
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCategorieService = new MockCategoryDataService();

            var categoryViewModel = new AddCategoryViewModel(mockNavigationService.Object, mockCategorieService, mockDialogService.Object);
            Assert.NotNull(categoryViewModel._category);
        }

        [Fact]
        public void Error_msg_if_category_name_null()
        {
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCategorieService = new MockCategoryDataService();

            var categoryViewModel = new AddCategoryViewModel(mockNavigationService.Object, mockCategorieService, mockDialogService.Object);
            categoryViewModel.OnAddNewCategoryAsync();
            Assert.NotNull(categoryViewModel.ACErrorMsg);
        }

        [Fact]
        public void Error_if_exception_thrown()
        {
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCategorieService = new MockCategoryDataService();

            var categoryViewModel = new AddCategoryViewModel(mockNavigationService.Object, mockCategorieService, mockDialogService.Object);
            categoryViewModel.AddCategoryName = "ExceptionThrow";
            categoryViewModel.OnAddNewCategoryAsync();
            Assert.NotNull(categoryViewModel.ACErrorMsg);
        }

        [Fact]
        public void No_error_msg_if_category_name_not_null()
        {
            var mockNavigationService = new Mock<INavigationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockCategorieService = new MockCategoryDataService();

            var categoryViewModel = new AddCategoryViewModel(mockNavigationService.Object, mockCategorieService, mockDialogService.Object);
            categoryViewModel.AddCategoryName = "Apple Pie";
            categoryViewModel.OnAddNewCategoryAsync();
            Assert.Null(categoryViewModel.ACErrorMsg);
        }
    }
}

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp;
using Windows.ApplicationModel.Appointments;
using System.Linq;

using MortalKombatFatalities.Navigation;
using MortalKombatFatalities.ViewModels;

namespace MortalKombatFatalities.Sections
{
    public class FatalitiesSection : Section<Fatalities1Schema>
    {
		private LocalStorageDataProvider<Fatalities1Schema> _dataProvider;

		public FatalitiesSection()
		{
			_dataProvider = new LocalStorageDataProvider<Fatalities1Schema>();
		}

		public override async Task<IEnumerable<Fatalities1Schema>> GetDataAsync(SchemaBase connectedItem = null)
        {
            var config = new LocalStorageDataConfig
            {
                FilePath = "/Assets/Data/Fatalities.json",
            };
            return await _dataProvider.LoadDataAsync(config, MaxRecords);
        }

        public override async Task<IEnumerable<Fatalities1Schema>> GetNextPageAsync()
        {
            return await _dataProvider.LoadMoreDataAsync();
        }

        public override bool HasMorePages
        {
            get
            {
                return _dataProvider.HasMoreItems;
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<Fatalities1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Fatalities1Schema>
                {
                    Title = "Fatalities",

                    Page = typeof(Pages.FatalitiesListPage),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Subtitle.ToSafeString();
                        viewModel.Description = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
						return NavInfo.FromPage<Pages.FatalitiesDetailPage>(true);
                    }
                };
            }
        }

        public override DetailPageConfig<Fatalities1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Fatalities1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Detail";
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<Fatalities1Schema>>
                {
                };

                return new DetailPageConfig<Fatalities1Schema>
                {
                    Title = "Fatalities",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}

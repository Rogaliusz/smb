
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using projekt_1.Activities.Products;
using projekt_1.Adapters.List;
using projekt_1.Models;
using projekt_1.Repositories;

namespace projekt_1.Adapters
{
    public class ShopListAdapter : ListAdapterBase<Shop>
    {
        public override Shop this[int position] => _shops[position];
        public override int Count => _shops.Count;

        private readonly IShopRepository _shopRepository;

        private IList<Shop> _shops;
        private Context _context;

        public ShopListAdapter(Context context, IShopRepository shopRepository)
        {
            _context = IoC.MainContainer.Container.Resolve<Context>();
            _shopRepository = shopRepository;

            RefreshData();
        }


        public override long GetItemId(int position)
            => _shops[position].Id.GetHashCode();

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ShopListAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ShopListAdapterViewHolder;

            if (holder == null)    
                CreateAndSetHolder(parent, out view, out holder, position);
           
            var product = _shops[position];

            holder.Name.Text = product.Name;
            holder.Description.Text = product.Description.ToString();
            holder.Round.Text = product.RoundSize.ToString();

            return view;
        }

        private void CreateAndSetHolder(ViewGroup parent, out View view, out ShopListAdapterViewHolder holder, int position)
        {
            holder = new ShopListAdapterViewHolder();
            var inflater = _context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            //replace with your item and your holder items
            //comment back in
            view = inflater.Inflate(Resource.Layout.shop_item_template, parent, false);

            holder.Name = view.FindViewById<TextView>(Resource.Id.txtName);
            holder.Description = view.FindViewById<TextView>(Resource.Id.txtDescription);
            holder.Round = view.FindViewById<TextView>(Resource.Id.txtRound);
            holder.Edit = view.FindViewById<TextView>(Resource.Id.txtEdit);
            holder.Delete = view.FindViewById<TextView>(Resource.Id.txtDelete);

            holder.Edit.Click += (s, e) => { GoToEditActivity(position); };
            holder.Delete.Click += async (s, e) => { await DeleteProductFromList(position); };

            view.Tag = holder;
        }

        private void GoToEditActivity(int position)
        {
            var id = _shops[position].Id;

            var intent = new Intent(_context, typeof(EditProductActivity));
            intent.PutExtra(common.Extras.ID, id.ToString());

            _context.StartActivity(intent);
        }

        private async Task DeleteProductFromList(int position)
        {
            var id = _shops[position].Id;

            await _shopRepository.DeleteAsync(id);

            RefreshData();
        }

        public async void RefreshData()
        {
            _shops = ( await _shopRepository.GetAllAsync()).ToList();
            NotifyDataSetChanged();
        }
    }

    

    public class ShopListAdapterViewHolder : Java.Lang.Object
    {
        public TextView Name { get; set; }
        public TextView Description { get; set; }
        public TextView Round { get; set; }

        public TextView Edit { get; set; }
        public TextView Delete { get; set; }
    }

}
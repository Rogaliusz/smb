using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Autofac;
using projekt_1.Activities.Products;
using projekt_1.Models;
using projekt_1.Repositories.Products;

namespace projekt_1.Adapters
{
    public class ProductListAdapter : BaseAdapter<Product>
    {
        public override Product this[int position] => _products[position];
        public override int Count => _products.Count;

        private readonly IProductRepository _productRepository;

        private IList<Product> _products;
        private Context _context;

        public ProductListAdapter(Context context)
        {
            this._context = context;
            _productRepository = IoC.MainContainer.Container.Resolve<IProductRepository>();

            RefreshData();
        }


        public override long GetItemId(int position)
            => _products[position].Id;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            ProductListAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ProductListAdapterViewHolder;

            if (holder == null)    
                CreateAndSetHolder(parent, out view, out holder);
           
            //fill in your items
            //holder.Title.Text = "new text here";
            var product = _products[position];

            holder.Name.Text = product.Name;
            holder.Price.Text = product.Price.ToString();
            holder.Count.Text = product.Count.ToString();

            holder.Purchased.Checked = product.Purchased;
            SetPurchasedCheckboxclickedHandler(position, holder);

            holder.Edit.Click += (s, e) => { GoToEditActivity(position); };
            holder.Delete.Click += (s, e) => { DeleteProductFromList(position); };

            return view;
        }

        private void CreateAndSetHolder(ViewGroup parent, out View view, out ProductListAdapterViewHolder holder)
        {
            holder = new ProductListAdapterViewHolder();
            var inflater = _context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            //replace with your item and your holder items
            //comment back in
            view = inflater.Inflate(Resource.Layout.product_item_template, parent, false);

            holder.Name = view.FindViewById<TextView>(Resource.Id.txtName);
            holder.Price = view.FindViewById<TextView>(Resource.Id.txtPrice);
            holder.Count = view.FindViewById<TextView>(Resource.Id.txtCount);
            holder.Edit = view.FindViewById<TextView>(Resource.Id.txtEdit);
            holder.Delete = view.FindViewById<TextView>(Resource.Id.txtDelete);
            holder.Purchased = view.FindViewById<CheckBox>(Resource.Id.cbPurchased);

            view.Tag = holder;
        }

        private void GoToEditActivity(int position)
        {
            var id = _products[position].Id;

            var intent = new Intent(_context, typeof(EditProductActivity));
            intent.PutExtra("ID", id.ToString());

            _context.StartActivity(intent);
        }

        private void DeleteProductFromList(int position)
        {
            var id = _products[position].Id;

            _productRepository.Delete(id);

            RefreshData();
        }

        private void SetPurchasedCheckboxclickedHandler(int position, ProductListAdapterViewHolder holder)
        {
            holder.Purchased.Click += (s, e) =>
            {
                var lProduct = _products[position];
                lProduct.Purchased = ((CheckBox)s).Checked;
                _productRepository.Update(lProduct);
            };
        }

        public void RefreshData()
        {
            _products = _productRepository.GetProducts().ToList();
            NotifyDataSetChanged();
        }
    }

    

    public class ProductListAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        public TextView Name { get; set; }
        public TextView Price { get; set; }
        public TextView Count { get; set; }

        public CheckBox Purchased { get; set; }

        public TextView Edit { get; set; }
        public TextView Delete { get; set; }
    }

}
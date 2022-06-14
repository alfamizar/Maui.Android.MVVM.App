using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Request;
using Maui.Android.MVVM.App.Platforms.Android.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AndroidX.RecyclerView.Widget.RecyclerView;
using View = Android.Views.View;

namespace Maui.Android.MVVM.App.Platforms.Android.Adapters
{
    public class ItemsAdapter : RecyclerView.Adapter
    {
        public List<User> UsersList { get; set; }
        public IItemClickListener ItemClickListener;
        public override int ItemCount => UsersList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Glide.With(holder.ItemView.Context)
                .Load(UsersList[position].PhotoUrl)
                .Apply(RequestOptions.CircleCropTransform())
                .Into((holder as MyViewHolder).UserPhotoImageView);
            (holder as MyViewHolder).UserFirstNameTextView.Text = UsersList[position].FirstName;
            (holder as MyViewHolder).UserLastNameTextView.Text = UsersList[position].LastName;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.UserCardView, parent, false);

            return new MyViewHolder(view, this);
        }

        public void SetOnClickListener(IItemClickListener itemClickListener)
        {
            this.ItemClickListener = itemClickListener;
        }

        class MyViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
        {
            public readonly ImageView UserPhotoImageView;
            public readonly TextView UserFirstNameTextView;
            public readonly TextView UserLastNameTextView;
            private readonly ItemsAdapter _itemsAdapter;
            public MyViewHolder(global::Android.Views.View itemView, ItemsAdapter adapter) : base(itemView)
            {
                UserPhotoImageView = itemView.FindViewById<ImageView>(Resource.Id.UserPhotoImageView);
                UserFirstNameTextView = itemView.FindViewById<TextView>(Resource.Id.UserFirstNameTextView);
                UserLastNameTextView = itemView.FindViewById<TextView>(Resource.Id.UserLastNameTextView);
                itemView.SetOnClickListener(this);
                _itemsAdapter = adapter;
            }

            public void OnClick(View v)
            {
                _itemsAdapter.ItemClickListener?.OnItemClicked(v, AbsoluteAdapterPosition);
            }
        }

        public interface IItemClickListener
        {
            void OnItemClicked(View view, int position);
        }
    }
}

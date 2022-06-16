using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Bumptech.Glide;
using Bumptech.Glide.Request;
using Maui.Android.MVVM.App.Platforms.Android.Models;
using View = Android.Views.View;

namespace Maui.Android.MVVM.App.Platforms.Android.Adapters
{
    public class UsersAdapter : RecyclerView.Adapter
    {
        public List<User> UsersList { get; set; }
        public IItemClickListener ItemClickListener;
        public override int ItemCount => UsersList.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Glide.With(holder.ItemView.Context)
                .Load(UsersList[position].Picture.Large)
                .Apply(RequestOptions.CircleCropTransform())
                .Into((holder as UserViewHolder).UserPhotoImageView);
            (holder as UserViewHolder).UserFirstNameTextView.Text = UsersList[position].Name.First;
            (holder as UserViewHolder).UserLastNameTextView.Text = UsersList[position].Name.Last;
            (holder as UserViewHolder).UserAgeTextView.Text = UsersList[position].Dob.Age.ToString();
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context)
                    .Inflate(Resource.Layout.user_card_view, parent, false);

            return new UserViewHolder(view, this);
        }

        public void SetOnClickListener(IItemClickListener itemClickListener)
        {
            ItemClickListener = itemClickListener;
        }

        class UserViewHolder : RecyclerView.ViewHolder, View.IOnClickListener
        {
            public readonly ImageView UserPhotoImageView;
            public readonly TextView UserFirstNameTextView;
            public readonly TextView UserLastNameTextView;
            public readonly TextView UserAgeTextView;
            private readonly UsersAdapter _itemsAdapter;

            public UserViewHolder(View itemView, UsersAdapter adapter) : base(itemView)
            {
                UserPhotoImageView = itemView.FindViewById<ImageView>(Resource.Id.user_photo_iv);
                UserFirstNameTextView = itemView.FindViewById<TextView>(Resource.Id.user_first_name_tv);
                UserLastNameTextView = itemView.FindViewById<TextView>(Resource.Id.user_last_name_tv);
                UserAgeTextView = itemView.FindViewById<TextView>(Resource.Id.user_age_tv);
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

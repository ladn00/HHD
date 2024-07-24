using HHD.DAL.Models;
using HHD.ViewModels;

namespace HHD.ViewMapper
{
    public class ViewMapper
    {
        public static PostModel MapPostViewModelToPostModel(PostViewModel viewmodel)
        {
            return new PostModel()
            {
                PostId = viewmodel.PostId,
                Title = viewmodel.Title ?? "",
                Intro = viewmodel.Intro ?? "",
                Status = viewmodel.Status,
            };
        }

        public static PostViewModel MapPostModelToPostViewModel(PostModel model)
        {
            return new PostViewModel()
            {
                PostId = model.PostId,
                Title = model.Title,
                Intro = model.Intro,
                Status = model.Status
            };
        }

        public static IEnumerable<PostContentItemViewModel> MapPostItemsModelToPostItemsViewModel(List<PostContentModel> items)
        {
            foreach (var model in items)
            {
                yield return new PostContentItemViewModel()
                {
                    PostContentId = model.PostContentId,
                    ContentItemType = model.ContentItemType,
                    Value = model.Value ?? "",

                };
            }
        }

        public static IEnumerable<PostContentModel> MapPostItemViewModelToPostItemModel(IEnumerable<PostContentItemViewModel> items, int postid)
        {
            foreach (PostContentItemViewModel model in items)
            {
                yield return new PostContentModel()
                {
                    PostContentId = model.PostContentId,
                    ContentItemType = (int)model.ContentItemType,
                    Value = model.Value ?? "",
                    PostId = postid
                };
            }
        }
    }
}

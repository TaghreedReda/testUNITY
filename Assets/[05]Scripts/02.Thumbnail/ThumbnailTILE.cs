using System.Collections;
using System.Collections.Generic;

namespace UIWidgets.Thumbnail
{
    using UIWidgets;
    using UnityEngine;
    using UnityEngine.UI;
    

    public class ThumbnailTILE : ListViewItem, IViewData<Thumbnailitem>
    {

        public override Graphic[] GraphicsForeground
        {
            get
            {
                return new Graphic[] { ThumbnailName, imgThumbNail };
            }
        }

        public Thumbnailitem Item;

        [SerializeField]
        public RawImage imgThumbNail;

        [SerializeField]
        public Text ThumbnailName;


        public ThumbnailTILEVIEW Tiles;

        public void Duplicate()
        {
            Tiles.DataSource.Add(Item);
        }

        /// <summary>
        /// Remove current item from TileView.DataSource.
        /// </summary>
        public void Remove()
        {
            Tiles.DataSource.RemoveAt(Index);
        }


        public void SetData(Thumbnailitem item)
        {
            Item = item;
            if (Item == null)
            {
                if (imgThumbNail != null)
                {
                    imgThumbNail.texture = null;
                }

                if (ThumbnailName != null)
                {
                    ThumbnailName.text = string.Empty;
                }


            }
            else
            {
                name = item.Name;

                if (imgThumbNail != null)
                {
                    imgThumbNail.texture = Item.ImgThumbnail.texture;
                }

                if (ThumbnailName != null)
                {
                    ThumbnailName.text = Item.ThumbnailName.text;
                }

            }

        }
    }

}
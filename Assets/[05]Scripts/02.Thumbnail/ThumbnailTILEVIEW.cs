using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UIWidgets.Thumbnail
{

    using UIWidgets;
    

    public class ThumbnailTILEVIEW : TileViewCustom<ThumbnailTILE, Thumbnailitem>
    {
        bool isInited = false;

        // Comparison<TileViewItemSample> itemsComparison = (x, y) => x.Name.CompareTo(y.Name);

        /// <summary>
        /// Awake this instance.
        /// </summary>
        protected override void Awake()
        {
            // OnSelect.AddListener(ItemSelected);
            // OnSelectObject.AddListener(ItemSelected);
        }

        void ItemSelected(int index, ListViewItem component)
        {
            if (component != null)
            {
                // (component as TileViewComponentSample).DoSomething();
            }

            Debug.Log(index);
            Debug.Log(DataSource[index].Name);
        }

        void ItemSelected(int index)
        {
            Debug.Log(index);
            Debug.Log(DataSource[index].Name);
        }

        /// <summary>
        /// Init this instance.
        /// </summary>
        public override void Init()
        {
            if (isInited)
            {
                return;
            }

            isInited = true;

            base.Init();

            // DataSource.Comparison = itemsComparison;
        }
    }
}

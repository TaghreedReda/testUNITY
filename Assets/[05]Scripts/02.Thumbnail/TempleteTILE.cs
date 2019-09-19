using System.Collections;
using System.Collections.Generic;


namespace UIWidgets.Thumbnail
{
    using UIWidgets;
    using UnityEngine;
    using UnityEngine.UI;

    public class TempleteTILE : ListViewItem, IViewData<TempleteItem>
    {
        public override Graphic[] GraphicsForeground
        {
            get
            {
                return new Graphic[] { TempleteName, TempletePath, Status };
            }
        }

        [SerializeField]
        public Image Icon;

        [SerializeField]
        public Text Name;

        [SerializeField]
        public Text TempleteName;

        [SerializeField]
        public Text TempletePath;

        [SerializeField]
        public Text Status;


        public TempleteItem Item;

        public bool SetNativeSize = true;


        public void SetData(TempleteItem item)
        {
            this.Item = item;
            if (Item == null)
            {
                if (Icon != null)
                {
                    Icon.sprite = null;
                }

                if (Name != null)
                {
                    Name.text = string.Empty;
                }

                if (TempleteName != null)
                {
                    TempleteName.text = string.Empty;
                }

                if (TempletePath != null)
                {
                    TempletePath.text = string.Empty;
                }


                if (Status != null)
                {
                    Status.text = string.Empty;
                }

            } else
            {
                this.name = item.Name;

                if (this.Icon != null)
                {
                    this.Icon.sprite = Item.Icon;
                }

                if (this.Name != null)
                {
                    this.Name.text = Item.Name;
                }


                if (this.TempletePath != null)
                {
                    this.TempletePath.text = Item.TempletePath;
                }

                if (this.Status != null)
                {
                    string AtmpSTATUS = "";

                    switch (Item.Status)
                    {
                        case 0:
                            AtmpSTATUS = "";
                            break;
                        case 1:
                            AtmpSTATUS = "State : Changed.";
                            break;
                        case 2:
                            AtmpSTATUS = "State : Edit.";
                            break;
                        default:
                            AtmpSTATUS = "";
                            break;
                    }

                    this.Status.text = AtmpSTATUS;


                }


                if (this.Icon != null)
                {
                    if (this.SetNativeSize)
                    {
                        this.Icon.SetNativeSize();
                    }

                    // set transparent color if no icon
                    this.Icon.color = (this.Icon.sprite == null) ? Color.clear : Color.white;
                }

            }

        }
    }
}
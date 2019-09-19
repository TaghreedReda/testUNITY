namespace UIWidgets.Thumbnail
{
	using System;
	using UnityEngine;
    using UnityEngine.UI;

    [Serializable]
	public class Thumbnailitem
    {
        [SerializeField]
        public string Name;

        [SerializeField]
        public RawImage ImgThumbnail;

        /// <summary>
        /// The name.
        /// </summary>
        [SerializeField]
        public Text ThumbnailName;


    }
}
﻿namespace EasyLayoutNS
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// LayoutElementInfo.
	/// Correctly works with multiple resizes during one frame.
	/// </summary>
	public class LayoutElementInfo
	{
		/// <summary>
		/// GameObject active state.
		/// </summary>
		public bool Active;

		/// <summary>
		/// RectTransform.
		/// </summary>
		public RectTransform Rect
		{
			get;
			protected set;
		}

		/// <summary>
		/// Width.
		/// </summary>
		public float Width
		{
			get;
			protected set;
		}

		/// <summary>
		/// Height.
		/// </summary>
		public float Height
		{
			get;
			protected set;
		}

		/// <summary>
		/// Size on main axis.
		/// </summary>
		public float AxisSize
		{
			get
			{
				return Layout.IsHorizontal ? Width : Height;
			}
		}

		/// <summary>
		/// Size on sub axis.
		/// </summary>
		public float SubAxisSize
		{
			get
			{
				return Layout.IsHorizontal ? Height : Width;
			}
		}

		/// <summary>
		/// Minimal width.
		/// </summary>
		public float MinWidth
		{
			get;
			protected set;
		}

		/// <summary>
		/// Minimal height.
		/// </summary>
		public float MinHeight
		{
			get;
			protected set;
		}

		/// <summary>
		/// Preferred width.
		/// </summary>
		public float PreferredWidth
		{
			get;
			protected set;
		}

		/// <summary>
		/// Preferred height.
		/// </summary>
		public float PreferredHeight
		{
			get;
			protected set;
		}

		/// <summary>
		/// Flexible width.
		/// </summary>
		public float FlexibleWidth
		{
			get;
			protected set;
		}

		/// <summary>
		/// Flexible height.
		/// </summary>
		public float FlexibleHeight
		{
			get;
			protected set;
		}

		/// <summary>
		/// Current layout.
		/// </summary>
		protected EasyLayout Layout;

		/// <summary>
		/// Scale.
		/// </summary>
		protected Vector3 Scale;

		/// <summary>
		/// Is width changed?
		/// </summary>
		public bool ChangedWidth
		{
			get;
			protected set;
		}

		/// <summary>
		/// Is height changed?
		/// </summary>
		public bool ChangedHeight
		{
			get;
			protected set;
		}

		/// <summary>
		/// New width.
		/// </summary>
		protected float newWidth;

		/// <summary>
		/// New width.
		/// </summary>
		public float NewWidth
		{
			get
			{
				return newWidth;
			}

			set
			{
				if (newWidth != value)
				{
					newWidth = value;
					Width = value * Scale.x;
					ChangedWidth = true;
				}
			}
		}

		/// <summary>
		/// New height.
		/// </summary>
		protected float newHeight;

		/// <summary>
		/// New height.
		/// </summary>
		public float NewHeight
		{
			get
			{
				return newHeight;
			}

			set
			{
				if (newHeight != value)
				{
					newHeight = value;
					Height = value * Scale.y;
					ChangedHeight = true;
				}
			}
		}

		/// <summary>
		/// Position of the top left corner.
		/// </summary>
		public Vector2 PositionTopLeft = Vector2.zero;

		/// <summary>
		/// Position with pivot.
		/// </summary>
		public Vector2 PositionPivot
		{
			get
			{
				return new Vector2(
					PositionTopLeft.x + (Width * Rect.pivot.x),
					PositionTopLeft.y - (Height * (1 - Rect.pivot.y)));
			}
		}

		/// <summary>
		/// Is position changed?
		/// </summary>
		public bool IsPositionChanged
		{
			get
			{
				var new_pos = PositionPivot;
				return (Rect.localPosition.x != new_pos.x) || (Rect.localPosition.y != new_pos.y);
			}
		}

		/// <summary>
		/// Set element.
		/// </summary>
		/// <param name="rectTransform">RectTransform.</param>
		/// <param name="active">Is gameobject active?</param>
		/// <param name="layout">Current layout.</param>
		public void SetElement(RectTransform rectTransform, bool active, EasyLayout layout)
		{
			Rect = rectTransform;
			Active = active;
			Layout = layout;

			Scale = rectTransform.localScale;
			Width = rectTransform.rect.width * Scale.x;
			Height = rectTransform.rect.height * Scale.y;

			newWidth = rectTransform.rect.width;
			newHeight = rectTransform.rect.height;

			ChangedWidth = false;
			ChangedHeight = false;

			if (Layout.ChildrenWidth != ChildrenSize.DoNothing || Layout.ChildrenHeight != ChildrenSize.DoNothing)
			{
				RefreshLayoutElements();
			}

			if (Layout.ChildrenWidth != ChildrenSize.DoNothing)
			{
				var sizes = GetWidthValues();
				MinWidth = sizes.Min;
				PreferredWidth = sizes.Preferred;
				FlexibleWidth = sizes.Flexible;
			}
			else
			{
				MinWidth = 0f;
				PreferredWidth = 0f;
				FlexibleWidth = 0f;
			}

			if (Layout.ChildrenHeight != ChildrenSize.DoNothing)
			{
				var sizes = GetHeightValues();
				MinHeight = sizes.Min;
				PreferredHeight = sizes.Preferred;
				FlexibleHeight = sizes.Flexible;
			}
			else
			{
				MinHeight = 0f;
				PreferredHeight = 0f;
				FlexibleHeight = 0f;
			}
		}

		/// <summary>
		/// Get widths values.
		/// </summary>
		/// <returns>Widths values.</returns>
		protected Size GetWidthValues()
		{
			if (Rect == null)
			{
				return default(Size);
			}

			if (Rect.gameObject.activeInHierarchy)
			{
				return new Size()
				{
					Min = Mathf.Max(0f, LayoutUtility.GetMinWidth(Rect)),
					Preferred = Mathf.Max(0f, LayoutUtility.GetPreferredWidth(Rect)),
					Flexible = Mathf.Max(0f, LayoutUtility.GetFlexibleWidth(Rect)),
				};
			}

			return GetLayoutWidths();
		}

		/// <summary>
		/// Get heights values.
		/// </summary>
		/// <returns>Height values.</returns>
		protected Size GetHeightValues()
		{
			if (Rect == null)
			{
				return default(Size);
			}

			if (Rect.gameObject.activeInHierarchy)
			{
				return new Size()
				{
					Min = Mathf.Max(0f, LayoutUtility.GetMinHeight(Rect)),
					Preferred = Mathf.Max(0f, LayoutUtility.GetPreferredHeight(Rect)),
					Flexible = Mathf.Max(0f, LayoutUtility.GetFlexibleHeight(Rect)),
				};
			}

			return GetLayoutHeights();
		}

		/// <summary>
		/// Set size.
		/// </summary>
		/// <param name="axis">Axis.</param>
		/// <param name="size">New size.</param>
		public void SetSize(RectTransform.Axis axis, float size)
		{
			if (axis == RectTransform.Axis.Horizontal)
			{
				NewWidth = size;
			}
			else
			{
				NewHeight = size;
			}
		}

#if UNITY_4_6 || UNITY_4_7
		/// <summary>
		/// Components list.
		/// </summary>
		protected List<Component> Components = new List<Component>();
#endif

		/// <summary>
		/// All ILayoutElements Of current gameobject.
		/// </summary>
		protected List<ILayoutElement> LayoutElements = new List<ILayoutElement>();

		/// <summary>
		/// Get widths from LayoutElements.
		/// </summary>
		/// <returns>Widths values.</returns>
		protected Size GetLayoutWidths()
		{
			var max_priority = MaxPriority(LayoutElements);

			var result = default(Size);

			foreach (var elem in LayoutElements)
			{
				if (elem.layoutPriority == max_priority)
				{
					result.Min = Mathf.Max(result.Min, elem.minWidth);
					result.Preferred = Mathf.Max(result.Preferred, Mathf.Max(elem.preferredWidth, elem.minWidth));
					result.Flexible = Mathf.Max(result.Flexible, elem.flexibleWidth);
				}
			}

			return result;
		}

		/// <summary>
		/// Refresh LayoutElements list.
		/// </summary>
		protected void RefreshLayoutElements()
		{
			LayoutElements.Clear();
#if UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_3_OR_NEWER
			Rect.GetComponents<ILayoutElement>(LayoutElements);
#else
			Components.Clear();
			Rect.GetComponents(typeof(ILayoutElement), Components);
			for (int i = 0; i < Components.Count; i++)
			{
				LayoutElements.Add(Components[i] as ILayoutElement);
			}
#endif
		}

		/// <summary>
		/// Get heights from LayoutElements.
		/// </summary>
		/// <returns>Heights values.</returns>
		protected Size GetLayoutHeights()
		{
			var max_priority = MaxPriority(LayoutElements);

			var result = default(Size);

			for (int i = 0; i < LayoutElements.Count; i++)
			{
				var elem = LayoutElements[i];
				if (elem.layoutPriority == max_priority)
				{
					result.Min = Mathf.Max(result.Min, elem.minHeight);
					result.Preferred = Mathf.Max(result.Preferred, Mathf.Max(elem.preferredHeight, elem.minHeight));
					result.Flexible = Mathf.Max(result.Flexible, elem.flexibleHeight);
				}
			}

			return result;
		}

		/// <summary>
		/// Get maximum priority from LayoutElements.
		/// </summary>
		/// <param name="elements">LayoutElements list.</param>
		/// <returns>Maximum priority.</returns>
		protected static int MaxPriority(List<ILayoutElement> elements)
		{
			if (elements.Count == 0)
			{
				return 0;
			}

			int result = elements[0].layoutPriority;
			for (int i = 1; i < elements.Count; i++)
			{
				result = Mathf.Max(result, elements[i].layoutPriority);
			}

			return result;
		}
	}
}
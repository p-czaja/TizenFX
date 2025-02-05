/*
* Copyright(c) 2019 Samsung Electronics Co., Ltd.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.Binding;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI.Components
{
    /// <summary>
    /// ButtonGroup is a group of buttons which can be set common property<br />
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ButtonGroup : BindableObject, global::System.IDisposable
    {
        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemheightProperty = BindableProperty.Create(nameof(Itemheight), typeof(float), typeof(ButtonGroup), 0.0f, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.SizeHeight = (float)newValue;
                }
                btGroup.itemheight = (float)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemheight;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemPointSizeProperty = BindableProperty.Create(nameof(ItemPointSize), typeof(float), typeof(ButtonGroup), 0.0f, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Text.PointSize = (float)newValue;
                }
                btGroup.itemPointSize = (float)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemPointSize;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemFontFamilyProperty = BindableProperty.Create(nameof(ItemFontFamily), typeof(string), typeof(ButtonGroup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Text.FontFamily = (string)newValue;
                }
                btGroup.itemFontFamily = (string)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemFontFamily;
        });

        /// This will be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemTextColorProperty = BindableProperty.Create(nameof(ItemTextColor), typeof(Color), typeof(ButtonGroup), Color.Black, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Text.TextColor = (Color)newValue;
                }
                btGroup.itemTextColor = (Color)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemTextColor;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemTextAlignmentProperty = BindableProperty.Create(nameof(ItemTextAlignment), typeof(HorizontalAlignment), typeof(ButtonGroup), new HorizontalAlignment(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Text.HorizontalAlignment = (HorizontalAlignment)newValue;
                }
                btGroup.itemTextAlignment = (HorizontalAlignment)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemTextAlignment;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty OverLayBackgroundColorSelectorProperty = BindableProperty.Create(nameof(OverLayBackgroundColorSelector), typeof(Selector<Color>), typeof(ButtonGroup), new Selector<Color>(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Overlay.BackgroundColor = (Selector<Color>)newValue;
                }
                btGroup.overLayBackgroundColorSelector = (Selector<Color>)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.overLayBackgroundColorSelector;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemBackgroundImageUrlProperty = BindableProperty.Create(nameof(ItemBackgroundImageUrl), typeof(string), typeof(ButtonGroup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    if (btn.Style.BackgroundImage == null)
                    {
                        btn.Style.BackgroundImage = new Selector<string>();
                    }              
                    btn.Style.BackgroundImage = (string)newValue;
                }
                btGroup.itemBackgroundImageUrl = (string)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemBackgroundImageUrl;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemBackgroundBorderProperty = BindableProperty.Create(nameof(ItemBackgroundBorder), typeof(Rectangle), typeof(ButtonGroup), new Rectangle(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {                 
                    btn.BackgroundImageBorder = (Rectangle)newValue;
                }
                btGroup.itemBackgroundBorder = (Rectangle)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemBackgroundBorder;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemShadowUrlProperty = BindableProperty.Create(nameof(ItemShadowUrl), typeof(string), typeof(ButtonGroup), string.Empty, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    if (btn.Style.Shadow.ResourceUrl == null)
                    {
                        btn.Style.Shadow.ResourceUrl = new Selector<string>();
                    }
                    btn.Style.Shadow.ResourceUrl = (string)newValue;
                }
                btGroup.itemShadowUrl = (string)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemShadowUrl;
        });

        /// This will be public opened in tizen_5.0 after ACR done. Before ACR, need to be hidden as inhouse API.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static readonly BindableProperty ItemShadowBorderProperty = BindableProperty.Create(nameof(ItemShadowBorder), typeof(Rectangle), typeof(ButtonGroup), new Rectangle(), propertyChanged: (bindable, oldValue, newValue) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            if (btGroup.itemGroup != null && newValue != null)
            {
                foreach (Button btn in btGroup.itemGroup)
                {
                    btn.Style.Shadow.Border = (Rectangle)newValue;
                }
                btGroup.itemShadowBorder = (Rectangle)newValue;
            }
        },
        defaultValueCreator: (bindable) =>
        {
            ButtonGroup btGroup = (ButtonGroup)bindable;
            return btGroup.itemShadowBorder;
        });

        [EditorBrowsable(EditorBrowsableState.Never)]
        public ButtonGroup(View view) : base()
        {
            itemGroup = new List<Button>();
            if ((root = view) == null)
            {
                throw new Exception("Root view is null.");
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int Count
        {
            get
            {
                return itemGroup.Count;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool Contains(Button bt)
        {
            return itemGroup.Contains(bt);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int GetIndex(Button bt)
        {
            return itemGroup.IndexOf(bt);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Button GetItem(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new Exception("button index error");
            }
            return itemGroup[index];
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddItem(Button bt)
        {
            if (itemGroup.Contains(bt))
            {
                return;
            }
            itemGroup.Add(bt);
            root.Add(bt);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveItem(Button bt)
        {
            if (!itemGroup.Contains(bt))
            {
                return;
            }
            itemGroup.Remove(bt);
            root.Remove(bt);
            bt.Dispose();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveItem(int index)
        {
            if (index >= Count || index < 0)
            {
                throw new Exception("button index error");
            }
            Button bt = itemGroup[index];
            itemGroup.Remove(bt);
            root.Remove(bt);
            bt.Dispose();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void RemoveAll()
        {
            foreach (Button bt in itemGroup)
            {
                root.Remove(bt);
                bt.Dispose();
            }
            itemGroup.Clear();
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void UpdateButton(ButtonStyle btStyle)
        {
            if (Count == 0) return;

            int buttonWidth = (int)root.Size.Width / Count;
            int buttonHeight = (int)itemheight;
            foreach (Button btnTemp in itemGroup)
            {
                btnTemp.Size = new Size(buttonWidth, buttonHeight);
            }

            int pos = 0;
            if (root.LayoutDirection == ViewLayoutDirectionType.RTL)
            {
                for (int i = Count - 1; i >= 0; i--)
                {
                    itemGroup[i].Style.PositionX = pos;
                    pos += (int)(itemGroup[i].Size.Width);
                }
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    itemGroup[i].Style.PositionX = pos;
                    pos += (int)(itemGroup[i].Size.Width);
                }
            }

            if (btStyle == null || btStyle.Text == null || btStyle.Text.TextColor == null) return;
            ItemTextColor = btStyle.Text.TextColor.All;
        }

        /// <summary>
        /// Common height for all of the Items
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public float Itemheight
        {
            get
            {
                return (float)GetValue(ItemheightProperty);
            }
            set
            {
                SetValue(ItemheightProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public float ItemPointSize
        {
            get
            {
                return (float)GetValue(ItemPointSizeProperty);
            }
            set
            {
                SetValue(ItemPointSizeProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ItemFontFamily
        {
            get
            {
                return (string)GetValue(ItemFontFamilyProperty);
            }
            set
            {
                SetValue(ItemFontFamilyProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Color ItemTextColor
        {
            get
            {
                return (Color)GetValue(ItemTextColorProperty);
            }
            set
            {
                SetValue(ItemTextColorProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public HorizontalAlignment ItemTextAlignment
        {
            get
            {
                return (HorizontalAlignment)GetValue(ItemTextAlignmentProperty);
            }
            set
            {
                SetValue(ItemTextAlignmentProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Selector<Color> OverLayBackgroundColorSelector
        {
            get
            {
                return (Selector<Color>)GetValue(OverLayBackgroundColorSelectorProperty);
            }
            set
            {
                SetValue(OverLayBackgroundColorSelectorProperty, value);
            }
        }

        /// <summary>
        /// The mutually exclusive with "backgroundColor" and "background" type Map.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ItemBackgroundImageUrl
        {
            get
            {
                return (string)GetValue(ItemBackgroundImageUrlProperty);
            }
            set
            {
                SetValue(ItemBackgroundImageUrlProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle ItemBackgroundBorder
        {
            get
            {
                return (Rectangle)GetValue(ItemBackgroundBorderProperty);
            }
            set
            {
                SetValue(ItemBackgroundBorderProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string ItemShadowUrl
        {
            get
            {
                return (string)GetValue(ItemShadowUrlProperty);
            }
            set
            {
                SetValue(ItemShadowUrlProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Rectangle ItemShadowBorder
        {
            get
            {
                return (Rectangle)GetValue(ItemShadowBorderProperty);
            }
            set
            {
                SetValue(ItemShadowBorderProperty, value);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Dispose()
        {
            if (disposed) return;
            if (itemGroup != null)
            {
                RemoveAll();
                itemGroup = null;
            }
            disposed = true;
        }

        private List<Button> itemGroup;
        private View root = null;
        private bool disposed = false;
        private float itemheight;
        private float itemPointSize;
        private string itemFontFamily;
        private Color itemTextColor = new Color();
        private HorizontalAlignment itemTextAlignment;
        private Selector<Color> overLayBackgroundColorSelector = new Selector<Color>();
        private string itemBackgroundImageUrl;
        private Rectangle itemBackgroundBorder = new Rectangle();
        private string itemShadowUrl;
        private Rectangle itemShadowBorder = new Rectangle();
    }
}

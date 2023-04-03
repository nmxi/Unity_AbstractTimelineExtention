using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace dev.kemomimi.TimelineExtension.AbstractValueControlTrack.Controller
{
    public class ImageController : AbstractColorValueController
    {
        [SerializeField] private List<Image> imageList;

        protected override void OnValueChanged(Color value)
        {
            foreach (var image in imageList)
            {
                image.color = value;
            }
        }
    }
}
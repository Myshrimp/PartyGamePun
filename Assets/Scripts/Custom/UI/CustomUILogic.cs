using UnityEngine;
using UnityGameFramework.Runtime;

namespace Party.Custom.UI
{
    public class CustomUILogic : UIFormLogic
    {
        protected UIForm m_ui_form;
        protected RectTransform m_rect_transform;
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            m_ui_form = GetComponent<UIForm>();
            m_rect_transform = m_ui_form.gameObject.GetComponent<RectTransform>();
            m_rect_transform.localPosition  =Vector3.zero;
        }
    }
}
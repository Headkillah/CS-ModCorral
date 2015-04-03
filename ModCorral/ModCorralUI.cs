﻿using System;
using System.Collections.Generic;
using System.Text;
using ICities;
using ColossalFramework;
using ColossalFramework.UI;
using UnityEngine;

namespace ModCorral
{
   public class ModCorralUI : UIPanel
   {
      public static float m_SafeMargin = 20f;
      public static float m_ShowHideTime = 0.3f;
      public bool m_Initialized = false;
      public Vector3 normalDisplayRelPos = Vector3.zero;

      public UITitleSubPanel TitleSubPanel;
      public UIScrollButtonPanel ScrollPanel;

      public void initialize()
      {
         float viewWidth = UIView.GetAView().GetScreenResolution().x;
         float viewHeight = UIView.GetAView().GetScreenResolution().y;

         this.size = new Vector2(383, 903);
         this.width = 383;
         this.height = 903;
         this.normalDisplayRelPos = new Vector3(viewWidth  - this.size.x, viewHeight - this.size.y, 0f);
         this.relativePosition = normalDisplayRelPos;

         this.backgroundSprite = "MenuPanel";

         this.clipChildren = false;
         this.canFocus = true;
         this.isInteractive = true;

         float inset = 5f;

         // title bar and close button
         TitleSubPanel = AddUIComponent<UITitleSubPanel>();
         TitleSubPanel.ParentPanel = this;
         TitleSubPanel.relativePosition = new Vector3(inset, inset, 0);
         TitleSubPanel.width = this.width;
         TitleSubPanel.height = 40;

         ScrollPanel = AddUIComponent<UIScrollButtonPanel>();
         ScrollPanel.ParentPanel = this;
         ScrollPanel.relativePosition = new Vector3(inset, TitleSubPanel.relativePosition.y + TitleSubPanel.height + inset);
         ScrollPanel.width = this.width;
         ScrollPanel.height = this.height - ScrollPanel.relativePosition.y - inset;

         m_Initialized = true;
      }

      public void ShowMeHideMe()
      {
         if (!this.isVisible)
         {
            ShowMe();
         }
         else
         {
            HideMe();
         }
      }
     
      public void ShowMe()
      {
         DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, string.Format("showme parentrelpos: {0} parentsize: {1} thisrelpos: {2} thissize: {3}", parent.relativePosition, parent.size, this.relativePosition, this.size));

         if (!m_Initialized)// || isVisible)
         {
            return;
         }

         float num = this.normalDisplayRelPos.x + this.size.x;
         float end = num - size.x;
         float start = num + m_SafeMargin;

         this.Show();

         ValueAnimator.Animate(this.GetType().ToString(), (Action<float>)(val =>
         {
            Vector3 relativePosition = this.relativePosition;
            relativePosition.x = val;
            relativePosition.y = this.normalDisplayRelPos.y;
            this.relativePosition = relativePosition;
            //this.closeToolbarButton.absolutePosition = this.m_CloseButton.absolutePosition;
         }), new AnimatedFloat(start, end, m_ShowHideTime, EasingType.ExpoEaseOut));

         //Singleton<AudioManager>.instance.PlaySound(this.m_SwooshInSound, 1f);
      }

      public void HideMe()
      {
         DebugOutputPanel.AddMessage(ColossalFramework.Plugins.PluginManager.MessageType.Message, string.Format("hideme parentrelpos: {0} parentsize: {1} thisrelpos: {2} thissize: {3}", parent.relativePosition, parent.size, this.relativePosition, this.size));

         if (!m_Initialized)// || !isVisible)
         {
            return;
         }

         float num = this.normalDisplayRelPos.x + this.size.x;
         float start = num - size.x;
         float end = num + m_SafeMargin;

         ValueAnimator.Animate(this.GetType().ToString(), (Action<float>)(val =>
         {
            Vector3 relativePosition = this.relativePosition;
            relativePosition.x = val;
            relativePosition.y = this.normalDisplayRelPos.y;
            this.relativePosition = relativePosition;
         }), new AnimatedFloat(start, end, m_ShowHideTime, EasingType.ExpoEaseOut), (Action)(() => this.Hide()));

      }
   }
}
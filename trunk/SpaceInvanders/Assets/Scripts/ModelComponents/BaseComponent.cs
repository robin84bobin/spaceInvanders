using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.ModelComponents
{
    public interface IBaseComponent 
    {
        void AddComponent(IBaseComponent child_);
        void RemoveComponent(IBaseComponent child_);
        void Remove();
        List<IBaseComponent> Children { get; }
        IBaseComponent Parent { get; set; }

        void Init();
        void Lock ();
        void Unlock ();
        void Release();
        void Update();
        bool IsSingle { get; }

        void SendMessage(string name_, params object[] paramList_);
    }

    public class BaseComponent : IBaseComponent 
    {
        private List<IBaseComponent> _children = new List<IBaseComponent> ();
        IBaseComponent _parent;
        public IBaseComponent Parent {
            get {
                return _parent;
            }
            set {
                _parent = value;
                OnSetParent();
            }
        }

        protected bool locked = true;

        public void AddComponent (IBaseComponent child_)
        {
            _children.Add (child_);
            child_.Parent = this;
            child_.Init ();
        }

        public void RemoveComponent (IBaseComponent child_)
        {
            if (_children.Contains (child_)) {
                _children.Remove(child_);
                child_.Release();
                child_ = null;
            }
        }

        public void Remove ()
        {
            if (Parent != null) {
                Parent.RemoveComponent (this);
            }

        }

        public List<IBaseComponent> Children {
            get {
                return _children;
            }
        }


        protected bool isSingle = true;	
        public bool IsSingle{
            get {
                return isSingle;
            }
        }


        public virtual void Init()
        {
            Unlock ();
            OnInit ();
        }

        public void Lock()
        {
            locked = true;
        }

        public void Unlock()
        {
            locked = false;
        }

        public void Release()
        {
            Lock ();
            _children.Clear();
            Parent = null;
            OnRelease ();
        }

        protected bool needToRemove = false;

        public void Update()
        {
            if (needToRemove) {
                Remove();
                return;
            }

            if (locked) {
                return;
            }

            OnUpdate ();

            for (int i = 0; i < _children.Count; i++) {
                _children[i].Update();
            }
        }

        Dictionary<string,MethodInfo> _methodsCache = new Dictionary<string, MethodInfo>();
        public void SendMessage (string name_, params object[] paramList_)
        {
            if (_methodsCache.ContainsKey (name_)) {
                _methodsCache[name_].Invoke (this, paramList_);
                return;
            }

            Type type = this.GetType ();
            MethodInfo method = type.GetMethod (name_);
            if (method == null) {
                Debug.LogWarning(String.Format ("Try to Invoke unexisted method {0}::{1}", this.GetType().Name, name_));
                return;
            }
            _methodsCache.Add (name_, method);
            method.Invoke (this, paramList_);
        }

        protected virtual void OnSetParent() {}
        protected virtual void OnUpdate (){}
        protected virtual void OnInit (){}
        protected virtual void OnRelease (){}

    }
}
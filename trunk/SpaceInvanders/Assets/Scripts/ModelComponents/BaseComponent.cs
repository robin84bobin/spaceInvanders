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
        Dictionary<Type, List<IBaseComponent>> Children { get; }
        List<IBaseComponent> GetChildren<T>() where T : IBaseComponent;
        IBaseComponent Parent { get; set; }

        void Init();
        void Lock ();
        void Unlock ();
        void Release();
        void Update();
        bool IsOneChildOfAKind { get; }
        bool NeedToRemove { get; }
        void SendMessage(string name_, params object[] paramList_);
    }

    public class BaseComponent : IBaseComponent 
    {
        private readonly Dictionary<Type,List<IBaseComponent>> _children = new Dictionary<Type, List<IBaseComponent>>();
        public List<IBaseComponent> GetChildren<T>() where T : IBaseComponent
        {
            Type type = typeof (T);
            if (_children.ContainsKey(type)) {
                return _children[type];
            }
            return null;
        }

        private readonly List<IBaseComponent> _unusedChildren = new List<IBaseComponent>();

        private IBaseComponent _parent;
        public IBaseComponent Parent {
            get {
                return _parent;
            }
            set {
                _parent = value;
                if (_parent != null) {
                    OnSetParent();
                    Debug.Log(_parent.GetType().Name + " -> addChild :: " + GetType().Name);
                }
            }
        }

        protected bool locked = true;

        public void AddComponent (IBaseComponent child_)
        {
            Type type = child_.GetType();

            if (!_children.ContainsKey(type)) {
                _children.Add(type, new List<IBaseComponent>());
            }
            else 
            if (child_.IsOneChildOfAKind && _children[type].Count > 0) {
                Debug.Log(GetType().Name + " couldn't attach more than one child of type: "+ type.Name);
                return;
            }
           _children[type].Add(child_);
            child_.Parent = this;
            child_.Init ();
        }

        public void RemoveComponent (IBaseComponent child_)
        {
            Type type = child_.GetType();
            if (_children.ContainsKey(type) && _children[type].Contains (child_)) {
                Debug.Log(GetType().Name + " -> removeChild :: " + child_.GetType().Name);
                _children[type].Remove(child_);
                child_.Release();
                child_ = null;
            }
        }

        public void Remove ()
        {
            NeedToRemove = true;
            /* Lock();
            if (Parent != null) {
                Parent.RemoveComponent (this);
            }*/
        }

        public Dictionary<Type, List<IBaseComponent>> Children {
            get {
                return _children;
            }
        }

        protected bool isOneChildOfAKind = true;	
        public bool IsOneChildOfAKind{
            get {
                return isOneChildOfAKind;
            }
        }

        public bool NeedToRemove {get; protected set;}

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
            Debug.Log(GetType().Name + " => Release");

            Lock ();
            foreach (KeyValuePair<Type, List<IBaseComponent>> list in _children)
                foreach (IBaseComponent child in list.Value) {
                    child.Release();
                }
            _children.Clear();
            Parent = null;
            OnRelease ();
        }

        public void Update()
        {
            if (NeedToRemove || locked) {
                return;
            }

            OnUpdate ();

            foreach (KeyValuePair<Type, List<IBaseComponent>> list in _children)
            {
                foreach (IBaseComponent child in list.Value)
                {
                    if (child.NeedToRemove)
                    {
                        _unusedChildren.Add(child);
                        continue;
                    }
                    child.Update();
                    //stop updating if this component had been locked or removed on its child's update
                    if (NeedToRemove || locked){
                        return;
                    }
                }
            }

            RemoveUnusedItems();
        }

        private void RemoveUnusedItems()
        {
            if (_unusedChildren.Count <= 0) {
                return;
            }

            foreach (IBaseComponent component in _unusedChildren) {
                RemoveComponent(component);
            }
            _unusedChildren.Clear();
        }

        readonly Dictionary<string,MethodInfo> _methodsCache = new Dictionary<string, MethodInfo>();
        


        public void SendMessage (string name_, params object[] paramList_)
        {
            if (_methodsCache.ContainsKey (name_)) {
                _methodsCache[name_].Invoke (this, paramList_);
                return;
            }

            Type type = GetType ();
            MethodInfo method = type.GetMethod (name_);
            if (method == null) {
                Debug.LogWarning(String.Format ("Try to Invoke unexisted method {0}::{1}", GetType().Name, name_));
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
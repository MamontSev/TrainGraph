using System;
using System.Collections.Generic;

using Mamont.UI.MVVM;

using UnityEngine;

using Zenject;

namespace Mamont.UI.MainMenu.Pages
{
	public interface IMainMenuPagesFactory
	{
		void CreatePage<T>() where T : IMainMenuPageViewModel;
	}
	public class MainMenuPagesFactory:MonoBehaviour, IMainMenuPagesFactory
	{
		private DiContainer _diContainer;
		[Inject]
		private void Construct
		(
			DiContainer _diContainer
		)
		{
			this._diContainer = _diContainer;
		}

		private void Awake()
		{
			InitPrefabs();
		}

		private readonly Dictionary<Type , GameObject> _pagePrefabDict = new();
		private void InitPrefabs()
		{

			_pagePrefabDict.Add(typeof(SelectLevelViewModel) , _selectLevelViewPrefab.gameObject);
		}

		[SerializeField]
		private Transform _pageContainer;
		[SerializeField]
		private SelectLevelView _selectLevelViewPrefab;

		
		private readonly Dictionary<Type , (IShowHideView view, IMainMenuPageViewModel viewModel)> _createdDict = new();


		private IShowHideView _currView = null;

		public event Action<Type> PageOpened;

		public void CreatePage<T>( ) where T : IMainMenuPageViewModel
		{
			_currView?.Hide();

			var t = typeof(T);

			if( _createdDict.ContainsKey(t) )
			{
				_currView = _createdDict[t].view;
			}
			else
			{
				_currView = CreateNew<T>();
			}

			_currView.Show();
			PageOpened?.Invoke(t);
		}

		private IShowHideView CreateNew<T>() where T : IMainMenuPageViewModel
		{
			var t = typeof(T);
			if( _pagePrefabDict.ContainsKey(t) == false )
			{
				throw new Exception($"Fail create Main menu Page {t.Name} not exist prefab");
			}

			GameObject obj = _diContainer.InstantiatePrefab(_pagePrefabDict[t] , _pageContainer);
			_diContainer.InjectGameObject(obj);

			IShowHideView newView = obj.GetComponent<IShowHideView>();

			IMainMenuPageViewModel newViewModel = _diContainer.Instantiate<T>();
			_diContainer.Inject(newViewModel);

			newView.Bind(newViewModel);

			_createdDict.Add(t , (newView, newViewModel));

			return newView;
		}
	}
}

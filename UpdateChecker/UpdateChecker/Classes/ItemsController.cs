﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.IO;

namespace UpdateChecker.Classes
{
    public class ItemsController
    {
        private List<SourceItem> sourcesCollection = new List<SourceItem>();
        private List<DestinationItem> destinationsCollection = new List<DestinationItem>();
        private ListBox sourceList;
        private ListBox destinationList;
        private Dictionary<int, List<DestinationItem>> usingDestinations = new Dictionary<int, List<DestinationItem>>();
        private int pathLeftMargin = 5;

        public ItemsController(ListBox sourceList, ListBox destinationList)
        {
            this.sourceList = sourceList;
            this.destinationList = destinationList;
        }

        public void AddSource(string path)
        {
            TextBox linkTextBox = new TextBox();
            TextBox pathTextBox = new TextBox();
            StackPanel stackPanel = new StackPanel();
            SourceItem sourceItem = new SourceItem(path, linkTextBox, pathTextBox);
            sourceItem.LinkChanged += LinkChangedHandler;
            sourceItem.PathChanged += PathChangedHandler;
            sourceItem.NeedUpdate += NeedUpdateHandler;
            sourcesCollection.Add(sourceItem);

			stackPanel.Width = sourceList.Width - 10;
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(linkTextBox);
            stackPanel.Children.Add(pathTextBox);

            linkTextBox.Width = 30;
            pathTextBox.Width = stackPanel.Width - linkTextBox.Width - pathLeftMargin;
            Thickness margin = new Thickness();
            margin.Left = pathLeftMargin;
            pathTextBox.Margin = margin;

            sourceList.Items.Add(stackPanel);
        }

        public void AddDestination(string path)
        {
            TextBox linkTextBox = new TextBox();
            TextBox pathTextBox = new TextBox();
            StackPanel stackPanel = new StackPanel();
            DestinationItem destinationItem = new DestinationItem(path, linkTextBox, pathTextBox);
			destinationItem.LinkChanged += LinkChangedHandler;
			destinationItem.PathChanged += PathChangedHandler;
            destinationsCollection.Add(destinationItem);

			stackPanel.Width = destinationList.Width - 10;
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(linkTextBox);
            stackPanel.Children.Add(pathTextBox);

            linkTextBox.Width = 30;
			pathTextBox.Width = stackPanel.Width - linkTextBox.Width - pathLeftMargin; 
            Thickness margin = new Thickness();
            margin.Left = pathLeftMargin;
            pathTextBox.Margin = margin;

            destinationList.Items.Add(stackPanel);
        }

		public void RemoveSource(int index)
		{
			StackPanel stackPanel = sourceList.Items.GetItemAt(index) as StackPanel;
			sourceList.Items.Remove(stackPanel);

			SourceItem item = sourcesCollection[index];
			stackPanel.Children.Remove(item.LinkTextBox);
			stackPanel.Children.Remove(item.PathTextBox);
			item.UnsubscribeFromEvents();
			item.LinkChanged -= LinkChangedHandler;
			item.PathChanged -= PathChangedHandler;
			item.NeedUpdate -= NeedUpdateHandler;
			sourcesCollection.Remove(item);
		}

		public void RemoveDestination(int index)
		{
			StackPanel stackPanel = destinationList.Items.GetItemAt(index) as StackPanel;
			destinationList.Items.Remove(stackPanel);

			DestinationItem item = destinationsCollection[index];
			stackPanel.Children.Remove(item.LinkTextBox);
			stackPanel.Children.Remove(item.PathTextBox);
			item.UnsubscribeFromEvents();
			item.LinkChanged -= LinkChangedHandler;
			item.PathChanged -= PathChangedHandler;
			destinationsCollection.Remove(item);

			List<DestinationItem> list;
			usingDestinations.TryGetValue(item.Link, out list);
			if (list != null)
			{
				list.Remove(item);
				if (list.Count == 0)
				{
					usingDestinations.Remove(item.Link);
				}
			}
		}

        private void LinkChangedHandler(object sender, EventArgs args)
        {
            if (sender is SourceItem)
            {
                SourceItem item = sender as SourceItem;
				int link;
				if (!item.LinkTextBox.Text.Equals(""))
				{
					link = int.Parse(item.LinkTextBox.Text);
				}
				else
				{
					return;
				}
                try
                {
                    foreach (SourceItem sItem in sourcesCollection)
                    {
                        if (item != sItem)
                        {
                            if (link == sItem.Link)
                            {
                                throw new LinkAlreadyExistException();
                            }
                        }
                    }
                    item.Link = link;
                }
                catch (LinkAlreadyExistException exception)
                {
                    MessageBox.Show(exception.Message);
                    item.LinkTextBox.Text = "";
                }
            }
            else if (sender is DestinationItem)
            {
                DestinationItem item = sender as DestinationItem;
				int link;
				if (!item.LinkTextBox.Text.Equals(""))
				{
					link = int.Parse(item.LinkTextBox.Text);
				}
				else
				{
					return;
				}
				item.Link = link;
                List<DestinationItem> list;
                usingDestinations.TryGetValue(link, out list);
                if (list != null)
                {
                    list.Add(item);
                }
                else
                {
                    usingDestinations.Add(link, new List<DestinationItem>());
                    usingDestinations[link].Add(item);
                }
            }
        }

        private void PathChangedHandler(object sender, EventArgs args)
        {
            Item item = sender as Item;
            item.Path = item.PathTextBox.Text;
        }

        private void NeedUpdateHandler(object sender, EventArgs args)
        {
            SourceItem item = sender as SourceItem;
            //string sourceFileName = Utils.GetFileName(item.Path);
            List<DestinationItem> linkedItems;
            usingDestinations.TryGetValue(item.Link, out linkedItems);
            if (linkedItems != null)
            {
                foreach (DestinationItem destItem in linkedItems)
                {
					try
					{
						File.Copy(item.Path, destItem.Path, true);
					}
					catch (Exception exception)
					{
						MessageBox.Show(exception.Message);
					}
                    
                }
            }
        }


    }
}

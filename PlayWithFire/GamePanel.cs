﻿using PlayWithFire.Interfaces;
using PlayWithFire.Services;
using PlayWithFire.Shapes.Creatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayWithFire
{
    public partial class GamePanel : Form
    {
        private List<Player> _players = new List<Player>();

        private IMapGeneratorService _mapGeneratorService;
        private MapService _mapService;
        public GamePanel()
        {
            _mapService = new MapService();
            _mapGeneratorService = new BasicMapGeneratorService();

            InitializeComponent();

            this.Size = new Size(1000, 1000);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.pbCanvas.Location = new Point(0, 0);
            this.pbCanvas.Size = this.ClientSize;

            // 10, 20, 30
            var shapeSize = new Size(50, 50);
            var shapeCount = new Size(this.pbCanvas.Size.Width / shapeSize.Width,
                this.pbCanvas.Size.Height / shapeSize.Height);

            _mapService.Map = _mapGeneratorService.CreateMap(shapeCount, shapeSize);

            var playerOne = new Player(
                new Point((shapeCount.Width - 2) * shapeSize.Width + shapeSize.Width / 4,
                shapeSize.Height + shapeSize.Height / 4),
                new Size(shapeSize.Width / 2, shapeSize.Height / 2),
                Brushes.Red,
                null
                );
            _players.Add(playerOne);

            this.pbCanvas.Refresh();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (_mapService.Map != null)
            {
                _mapService.DrawMap(_mapService.Map, e.Graphics);
            }

            _players.ForEach(p =>
            {
                p.Draw(e.Graphics);
            });
        }

        private void GamePanel_KeyDown(object sender, KeyEventArgs e)
        {
            var x = e.KeyCode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Day22
{
    public class Combat : IDay
    {
        public int DayNumber => 22;
        public string ValidatedPart1 => "33680";
        public string ValidatedPart2 => string.Empty;

        private readonly Deck _playerOne;
        private readonly Deck _playerTwo;

        private Combat(Deck playerOne, Deck playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

        public static Combat LoadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename).ToList();
            lines.Add(string.Empty);

            var decks = new List<Deck>();
            var player = 1;
            Deck currentDeck = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("Player"))
                {
                    currentDeck = new Deck(player);
                    player += 1;
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    decks.Add(currentDeck);
                }
                else
                {
                    currentDeck.AddToBottom(int.Parse(line));
                }
            }

            return new Combat(decks[0], decks[1]);
        }

        public string Part1()
        {
            while (!_playerOne.IsEmpty() && !_playerTwo.IsEmpty())
            {
                var winner = _playerOne;
                var loser = _playerTwo;

                if (_playerTwo.NextCard() > _playerOne.NextCard())
                {
                    winner = _playerTwo;
                    loser = _playerOne;
                }

                winner.AddToBottom(winner.TakeCard());
                winner.AddToBottom(loser.TakeCard());
            }

            var gameWinner = _playerOne.IsEmpty() ? _playerTwo : _playerOne;

            return gameWinner.GetScore().ToString();
        }

        public string Part2()
        {
            return string.Empty;
        }

        private class Deck
        {
            private Queue<int> _deck = new Queue<int>();
            private int _player;
            public Deck(int player)
            {
                _player = player;
            }

            public void AddToBottom(int card) => _deck.Enqueue(card);

            public int TakeCard() => _deck.Dequeue();
            public int NextCard() => _deck.Peek();

            public bool IsEmpty() => !_deck.Any();

            public int GetScore()
            {
                var result = 0;
                var multiplier = _deck.Count;
                foreach (var card in _deck)
                {
                    result += card * multiplier;
                    multiplier -= 1;
                }

                return result;
            }
        }
    }
}

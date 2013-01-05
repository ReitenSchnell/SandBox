using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace BowlingKata
{
    public class Frame
    {
        public Frame(int firstRoll, int secondRoll)
        {
            FirstRoll = firstRoll;
            SecondRoll = secondRoll;
        }

        public int FirstRoll { get; set; }
        public int SecondRoll { get; set; }
        public int FrameSum 
        { 
            get { return FirstRoll + SecondRoll; }
        }

        public bool IsSpare
        {
            get { return FrameSum == 10 && FirstRoll < 10; }
        }

        public bool IsStrike
        {
            get { return FirstRoll == 10; }
        }

        public bool IsSimple
        {
            get { return !IsSpare && !IsStrike; }
        }
    }

    public class Bowling
    {
        public int GetGameScore(List<Frame> frames)
        {
            AddTwoSinteticRolls(frames);
            var gameFrames = frames.Take(10).ToList();
            var simpeRollsSum = gameFrames.Where(frame => frame.IsSimple).Sum(frame => frame.FrameSum);
            var spareRollsSum = gameFrames.Where(frame => frame.IsSpare).Sum(frame => frame.FrameSum + frames[frames.IndexOf(frame) + 1].FirstRoll);
            var strikeRollsSum = gameFrames.Where(frame => frame.IsStrike).SelectMany(frame => frames.Skip(frames.IndexOf(frame)).Take(3)).Sum(frame => frame.FrameSum);
            return simpeRollsSum + spareRollsSum + strikeRollsSum;
        }

        private static void AddTwoSinteticRolls(List<Frame> frames)
        {
            frames.AddRange(Enumerable.Range(0, 2).Select(i => new Frame(0, 0)));
        }
    }

    public class BowlingTests
    {
        private readonly  Bowling bowling = new Bowling();
        private readonly List<Frame> frames;

        public BowlingTests()
        {
            frames = Enumerable.Range(0, 10).Select(i => new Frame(0, 0)).ToList();
        }

        [Fact]
        public void all_ones()
        {
            frames.ForEach(frame =>
                               {
                                   frame.FirstRoll = 1;
                                   frame.SecondRoll = 1;
                               });

            var result = bowling.GetGameScore(frames);

            result.Should().Be(20);
        }
        
        [Fact]
        public void all_rolls_are_simple()
        {
            frames.ForEach(frame =>
                               {
                                   frame.FirstRoll = 1;
                                   frame.SecondRoll = 2;
                               });

            var result = bowling.GetGameScore(frames);

            result.Should().Be(30);
        }

        [Fact]
        public void ten_pairs_of_9_and_miss()
        {
            frames.ForEach(frame => frame.FirstRoll = 9);

            var result = bowling.GetGameScore(frames);

            result.Should().Be(90);
        }

        [Fact]
        public void one_spare_at_the_beginning()
        {
            frames[0].FirstRoll = 5;
            frames[0].SecondRoll = 5;
            frames[1].FirstRoll = 3;

            var result = bowling.GetGameScore(frames);

            result.Should().Be(16);
        }

        [Fact]
        public void one_strike_at_the_beginning()
        {
            frames[0].FirstRoll = 10;
            frames[1].FirstRoll = 3;
            frames[2].FirstRoll = 4;

            var result = bowling.GetGameScore(frames);

            result.Should().Be(24);
        }

        [Fact]
        public void ten_spares_with_final_spare()
        {
            frames.ForEach(frame =>
                               {
                                   frame.FirstRoll = 5;
                                   frame.SecondRoll = 5;
                               });
            frames.Add(new Frame(5, 0));

            var result = bowling.GetGameScore(frames);

            result.Should().Be(150);
        }

        [Fact]
        public void ten_strikes_with_2_final_strikes()
        {
            frames.AddRange(Enumerable.Range(0, 2).Select(i => new Frame(0, 0)));
            frames.ForEach(frame =>
            {
                frame.FirstRoll = 10;
            });
            
            var result = bowling.GetGameScore(frames);

            result.Should().Be(300);
        }

        [Fact]
        public void real_game()
        {
            frames.Add(new Frame(0, 0));
            frames[0].FirstRoll = 1;
            frames[0].SecondRoll = 4;
            frames[1].FirstRoll = 4;
            frames[1].SecondRoll = 5;
            frames[2].FirstRoll = 6;
            frames[2].SecondRoll = 4;
            frames[3].FirstRoll = 5;
            frames[3].SecondRoll = 5;
            frames[4].FirstRoll = 10;
            frames[5].FirstRoll = 0;
            frames[5].SecondRoll = 1;
            frames[6].FirstRoll = 7;
            frames[6].SecondRoll = 3;
            frames[7].FirstRoll = 6;
            frames[7].SecondRoll = 4;
            frames[8].FirstRoll = 10;
            frames[9].FirstRoll = 2;
            frames[9].SecondRoll = 8;
            frames[10].FirstRoll = 6;

            var result = bowling.GetGameScore(frames);

            result.Should().Be(149);
        }
    }
}
